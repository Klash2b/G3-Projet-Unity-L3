using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPowerUp : MonoBehaviour
{
    public static int collected = 0;
    public Text txt;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "PowerUp")
        {
            Destroy(otherCollider.gameObject);
        }
    }
}

