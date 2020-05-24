using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FallingEnemy : MonoBehaviour
{

    public float speed = 37f;
    public float maxSpeed = 75f;
    public float returnSpeed = 12f;

    private BoxCollider2D box;
    private Rigidbody2D rigidbodyComponent;

    private float startingY;

    private float pos;
    private float scaleX;

    //Demi hauteur du boxcollider (distance entre le centre et le bas), nécessaire pour savoir si l'on peut sauter
    //(on y ajoute un Epsilon pour savoir si l'on est en contact avec le sol, car les Raycasts partent du centre du collider)
    private float demiHauteur; 
    private bool active = false;
    private bool off = false;

    private float largeur;

    // Start is called before the first frame update
    void Start()
    {
        if (box == null) box = GetComponent<BoxCollider2D>();
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>(); 
        largeur = box.size.x;
        startingY = transform.position.y;
        scaleX = transform.localScale.x;
        demiHauteur = box.bounds.extents.y;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = transform.position.y;
        

        if (!(active || estAuSol() || off))
        {
            active = joueurDessous();
            if (active)
            {
                SoundEffectsHelper.Instance.MakeFallingEnemySound();
            }
        }
        else if (active && !off)
        {

            if (Mathf.Abs(rigidbodyComponent.velocity.y)<maxSpeed)
            {
                rigidbodyComponent.AddForce(new Vector2(0, -speed));
            }
        }

        if (estAuSol())
        {
            off = true;
        }
        

        
        if (off)
        {
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

        /*On fait partir le raycast de plus haut que la position de l'ennemi, car sinon, lorsque le joueur se
        situe à une altitude proche de l'ennemi, la détection ne fonctionne pas bien car le raycast part parfois
        plus bas que le joueur.
        On est donc obligé de faire un RaycastAll car le raycast part du dessus de l'ennemi, et peut 
        rencontrer d'autres objets que le joueur en chemin, et ceux-ci l'arrêteraient autrement*/
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position + new Vector3(0,15f,0),
        scaleX*largeur+7.5f, -1f*Vector2.up);
        
        /*On vérifie d'abord que le tableau des objets touchés par le raycast ne vaut pas null pour
        ne pas tester sur un tableau vide, puis on vérifie que le joueur fait partie des objets touchés*/
        return hit != null && Array.Exists(hit, element => element.transform.tag == "Player");
    }

    public bool estAuSol()
    {
        return Physics2D.Raycast(transform.position, -1f*Vector2.up, demiHauteur+0.1f);
    }
}
