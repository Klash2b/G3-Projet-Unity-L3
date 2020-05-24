using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponScript weapon;
    public Camera cam;
    private float xMax;
    private float yMax;
    private float xMin;
    private float yMin;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private Vector3 viewPos;

    public float speed = 12;
    // Use this for initialization
    void Start()
    {
        xMax = transform.position.x + 7f;
        yMax = transform.position.y + 7f;
        xMin = transform.position.x - 7f;
        yMin = transform.position.y - 7f;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        Physics2D.IgnoreLayerCollision(8,9);
    }
    void FixedUpdate()
    {
        viewPos = cam.WorldToViewportPoint(transform.position);

        /*Ce bloc de code permet de "décaler" la détection de l'ennemi par la caméra,
        pour éviter que celui-ci s'arrête de bouger dès qu'il atteint l'extremité de l'écran*/
        if (viewPos.x <= 1)
        {
            viewPos = cam.WorldToViewportPoint(transform.position + new Vector3(2.5f,0,0));
        }
        else if (viewPos.x >= 0)
        {
            viewPos = cam.WorldToViewportPoint(transform.position + new Vector3(-2.5f,0,0));
        }
        if (viewPos.y <= 1)
        {
            viewPos = cam.WorldToViewportPoint(transform.position + new Vector3(0,2.5f,0));
        }
        if (viewPos.y >= 0)
        {
            viewPos = cam.WorldToViewportPoint(transform.position + new Vector3(0,-2.5f,0));
        }

        float x, y;
        if (viewPos.x <= 1 && viewPos.x >= 0 && viewPos.y <= 1 && viewPos.y >= 0)
        {
            /*On change le mouvement aléatoire toutes les 10 frames pour éviter
              que l'ennemi se déplace par "spasmes"*/
            if (Time.frameCount % 10 == 0)
            {
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);

                if (transform.position.x > xMax)
                {
                    x = Random.Range(-1f, 0f);
                }
                else if (transform.position.x < xMin)
                {
                    x = Random.Range(0f, 1f);
                }
                if (transform.position.y > yMax)
                {
                    y = Random.Range(-1f, 0f);
                }
                else if (transform.position.y < yMin)
                {
                    y = Random.Range(0f, 1f);
                }
                Vector3 movement = new Vector3(x, y, 0f).normalized * speed;
                GetComponent<Rigidbody2D>().velocity = movement;
            }
            if (weapon.CanAttack)
            {
                Vector3 direction = cam.transform.position - transform.position;
                weapon.Attack(direction);
                SoundEffectsHelper.Instance.MakeShotSound();
            }
            
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            weapon.shootCooldown = weapon.shootingRate;
        }
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(scaleX),
                scaleY, scaleZ);
        }
 
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f*Mathf.Abs(scaleX),
                scaleY, scaleZ);
        }
    }
}
