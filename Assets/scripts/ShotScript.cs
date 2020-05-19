using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8,9);
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 10); // 10sec
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Player" && col.gameObject.tag != "FlyingEnemy")
        {
            Destroy(gameObject);
        }
    }


}