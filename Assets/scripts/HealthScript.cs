using System.Collections;
using System.Collections.Generic;
using System;
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
    private float dieTime;

    public Animator anim;

    /// <summary>
    /// Enemy or player?
    /// </summary>

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    void Start()
    {
       AnimationClip death = Array.Find(anim.runtimeAnimatorController.animationClips,
       clip => clip.name == "die" || clip.name == "gDie" || clip.name == "fDie" || clip.name == "fDieNight" || clip.name == "gDieNight");
       dieTime = death.length;
    }
    public void damage(int damageCount)
    {
        hp -= damageCount;
        if (gameObject.tag == "Player" && !(isRespawning ||anim.GetBool("isDying")))
        {
            SoundEffectsHelper.Instance.MakePlayerDamageSound();
        }

        if (hp <= 0)
        {
            hp = 0;

            //Si le joueur a au moins ramassé un objet checkpoint
            if (gameObject.tag == "Player" 
            && !CheckpointScript.getLastCheckpoint().Equals(new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity))
            && !isRespawning)
            //On vérifie aussi que le joueur n'est pas en attente de réapparition
            {
                transform.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                isRespawning = true;

                /*On ne sait pas quel script de mouvement est actuellement activé, donc on 
                va vérifier quel script est activé, le désactiver, et appeler la coroutine
                en mettant ce script en paramètre pour le réactiver après réapparition du joueur*/
                if (gameObject.GetComponent<PlayerScript>() && gameObject.GetComponent<PlayerScript>().enabled)
                //On vérifie que le script est rattaché au joueur, puis qu'il est activé
                {
                    gameObject.GetComponent<PlayerScript>().enabled = false;
                    StartCoroutine(Respawn(gameObject.GetComponent<PlayerScript>()));
                }
                if (gameObject.GetComponent<PlayerScript2>() && gameObject.GetComponent<PlayerScript2>().enabled)
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
                    anim.SetBool("isDying", true);
                    if (gameObject.tag == "Player")
                    {
                        Collected.setCollected(0);
                    }
                    Destroy(gameObject, dieTime);
                }
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "FallingEnemy" || col.gameObject.tag == "Spike" || col.gameObject.tag == "InvisWall")
        {
            damage(hp);
        }

        if (gameObject.tag == "Player")
        {
            if (col.gameObject.tag == "Goomba" || col.gameObject.tag == "FlyingEnemy")
            {
                if (!col.gameObject.GetComponent<Animator>().GetBool("isDying"))
                {
                    damage(2);
                    Vector2 knockBack = new Vector2(0,0);

                    //Si le joueur est immobile, le knockback se fait dans la direction opposée au mouvement de l'ennemi
                    if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                    {
                        knockBack.x = -20f*Mathf.Sign(col.gameObject.GetComponent<Rigidbody2D>().velocity.x);
                    }
                    //Sinon, il se fait dans la direction opposée au mouvement du joueur
                    else
                    {
                        knockBack.x = -20f*Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.x);
                    }
                    if(col.gameObject.tag == "FlyingEnemy")
                    {
                        knockBack.y = -20f*Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        knockBack.y = 20f;
                    }
                    GetComponent<Rigidbody2D>().velocity = knockBack;
                    GetComponent<Rigidbody2D>().AddForce(knockBack);
                    col.gameObject.GetComponent<HealthScript>().damage(col.gameObject.GetComponent<HealthScript>().hp);
                }
                
            }
            if(col.gameObject.tag == "Shot")
            {
                damage(shotDamage);
                Destroy(col.gameObject);
            }
        }
    }

    void OnDestroy()
    {
        if (gameObject.tag == "Player"){
            // Récupération du script GameOverScript
            var GameOverCanvas = FindObjectOfType<GameOverScript>();
            // Affichage des boutons et du texte de Game Over
            GameOverCanvas.EnableText();
            GameOverCanvas.ShowButtons();
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
        // Récupération du script RespawnCanvasScript
        var RespawnCanvas = FindObjectOfType<RespawnCanvasScript>();
        // Affichage du Canvas "Réapparition dans 1.5s"
        RespawnCanvas.EnableText();

        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(dieTime);
        gameObject.GetComponent<Renderer>().enabled = false;

        //Le joueur perd des pièces en fonction de la difficulté lorsqu'il meurt
        if (MenuScript.getDifficulty() == "easy")
        {
            Collected.setCollected(Collected.getCollected()-2);
        }
        else if (MenuScript.getDifficulty() == "normal")
        {
            Collected.setCollected(Collected.getCollected()-3);
        }
        else 
        {
            Collected.setCollected(Collected.getCollected()-5);
        }

        anim.SetBool("isDying", false);
        yield return new WaitForSeconds(1.5f);

        // Effacement du Texte "Réapparition dans 1.5s"
        RespawnCanvas.DisableText();

        hp = 5;
        gameObject.GetComponent<Renderer>().enabled = true;
        p.enabled = true;
        isRespawning = false;
        transform.position = CheckpointScript.getLastCheckpoint();
    }
}