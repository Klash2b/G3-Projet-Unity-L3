﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private float demiHauteur;
    private float demiLargeur;

    void Update()
    {
        // 2 - Movement
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
        demiHauteur = GetComponent<BoxCollider2D>().bounds.extents.y;
        demiLargeur = GetComponent<BoxCollider2D>().bounds.extents.x;
    }

    void FixedUpdate()
    {

        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;


        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply movement to the rigidbody
        if (finPlateforme())
        {
            direction.x = -1f*direction.x;
            rigidbodyComponent.velocity = new Vector2(-movement.x, rigidbodyComponent.velocity.y);
        }
        else
        {
            rigidbodyComponent.velocity = new Vector2(movement.x, rigidbodyComponent.velocity.y);
        }

        if (direction.x > 0)
        {   
            transform.localScale = new Vector3(Mathf.Abs(scaleX),
             scaleY, scaleZ);
        }
    
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1f*Mathf.Abs(scaleX),
             scaleY, scaleZ);
        }
        
        
    }

    public bool finPlateforme()
    {
        bool b = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(Mathf.Sign(direction.x)*0.05f,0,0),
         -1f*Vector2.up, demiHauteur+0.1f);
        
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position,
         -1f*Vector2.up, demiHauteur+0.1f);

        RaycastHit2D testMur = Physics2D.Raycast(transform.position,
         Mathf.Sign(direction.x)*Vector2.right, demiLargeur+0.05f);

        if (testMur)
        {
            b = testMur.transform.tag != "Wall";
        }

        return !(hit && hit.transform.tag != "Player" && hit2 && hit2.transform.tag != "Player" && b);
    }
}