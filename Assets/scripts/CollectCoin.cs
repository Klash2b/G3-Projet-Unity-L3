using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public Text txt;

    void Start()
    {
        txt.text = "Pièces : " + Collected.collected.ToString();
    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.tag == "Coin")
        {
            Collected.collected++;
            Destroy(otherCollider.gameObject);
        }

        txt.text = "Pièces : " + Collected.collected.ToString();
    }
}

