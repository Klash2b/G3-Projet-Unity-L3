using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 5;
    private int shotDamage = 1;
    private bool isRespawning;

    /// <summary>
    /// Enemy or player?
    /// </summary>

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            hp = 0;

            //Si le joueur a au moins ramassé un objet checkpoint
            if (gameObject.tag == "Player" 
            && !LastCheckpoint.getLastCheckpoint().Equals(new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity))
            && !isRespawning)
            //On vérifie aussi que le joueur n'est pas en attente de réapparition
            {
                transform.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Renderer>().enabled = false;
                isRespawning = true;

                /*On ne sait pas quel script de mouvement est actuellement activé, donc on 
                va vérifier quel script est activé, le désactiver, et appeler la coroutine
                en mettant ce script en paramètre pour le réactiver après réapparition du joueur*/
                if (gameObject.GetComponent<PlayerScript>().enabled)
                {
                    gameObject.GetComponent<PlayerScript>().enabled = false;
                    StartCoroutine(Respawn(gameObject.GetComponent<PlayerScript>()));
                }
                if (gameObject.GetComponent<PlayerScript2>().enabled)
                {
                    gameObject.GetComponent<PlayerScript2>().enabled = false;
                    StartCoroutine(Respawn( gameObject.GetComponent<PlayerScript2>()));
                }
            }
            else
            {
                // Dead!
                if (!(gameObject.tag == "Player" && isRespawning))
                {
                    Destroy(gameObject);
                }   
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "FallingEnemy" || col.gameObject.tag == "Spike")
        {
            damage(hp);
        }

        if (gameObject.tag == "Player")
        {
            if (col.gameObject.tag == "Goomba" || col.gameObject.tag == "FlyingEnemy")
            {
                damage(2);
                Vector2 knockBack = col.gameObject.GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = -2f*knockBack;
                GetComponent<Rigidbody2D>().AddForce(-1f*knockBack);
                Destroy(col.gameObject);
            }
            if(col.gameObject.tag == "Shot")
            {
                damage(shotDamage);
                Destroy(col.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "Goomba" || gameObject.tag == "FlyingEnemy")
        {
            if (col.gameObject.tag == "Weapon")
            {
                damage(1);
            }
        }
        
    }

    private IEnumerator Respawn(MonoBehaviour p)
    {
        yield return new WaitForSeconds(2);
        hp = 5;
        gameObject.GetComponent<Renderer>().enabled = true;
        p.enabled = true;
        isRespawning = false;
        transform.position = LastCheckpoint.getLastCheckpoint();
        
    }
}