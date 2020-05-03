﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : MonoBehaviour
{

    public float speed = 37f;
    public float maxSpeed = 75f;
    public float returnSpeed = 12f;

    private BoxCollider2D box;
    private Rigidbody2D rigidbodyComponent;

    public float startingY;

    public float pos;

    //Demi hauteur du boxcollider (distance entre le centre et le bas), nécessaire pour savoir si l'on peut sauter
    //(on y ajoute un Epsilon pour savoir si l'on est en contact avec le sol, car les Raycasts partent du centre du collider)
    private float demiHauteur; 
    private bool active = false;
    private bool auSol = false;
    private bool off = false;

    private float largeur;

    // Start is called before the first frame update
    void Start()
    {
        if (box == null) box = GetComponent<BoxCollider2D>();
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>(); 
        largeur = box.size.x;
        startingY = transform.position.y;
        demiHauteur = box.bounds.extents.y;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = transform.position.y;
        auSol = estSol();
        

        if (!(active || auSol) && !off)
        {
            active = joueurDessous();
        }
        else if (active && !(auSol || off))
        {

            if (Mathf.Abs(rigidbodyComponent.velocity.y)<maxSpeed)
            {
                rigidbodyComponent.AddForce(new Vector2(0, -speed));
            }
        }
        
        
        if (auSol || off)
        {
            off = true;
            if (pos < startingY)
            {
                rigidbodyComponent.velocity = new Vector2 (0, returnSpeed);
            }
            else
            {
                rigidbodyComponent.velocity = new Vector2 (0, 0);
                off = false;
            }
            
            active = false;  
        }
    }

    public bool joueurDessous()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, largeur, -1f*Vector2.up);
        return hit.transform.tag == "Player";
    }

    public bool estSol()
    {
        return Physics2D.Raycast(transform.position, -1f*Vector2.up, demiHauteur+0.1f);
    }
}
