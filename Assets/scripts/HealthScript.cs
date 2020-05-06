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
            // Dead!
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "FallingEnemy" || col.gameObject.tag == "Spike")
        {
            damage(hp);
        }
        if (col.gameObject.tag == "Goomba")
        {
            damage(2);
            Vector2 knockBack = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = -2f*knockBack;
            GetComponent<Rigidbody2D>().AddForce(-1f*knockBack);
            Destroy(col.gameObject);
        }
    }
}