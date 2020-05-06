using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPowerUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "PowerUp")
        {
            Destroy(otherCollider.gameObject);
            gameObject.GetComponent<PlayerScript>().enabled = false;
            gameObject.GetComponent<PlayerScript2>().enabled = true;
        }
    }
}

