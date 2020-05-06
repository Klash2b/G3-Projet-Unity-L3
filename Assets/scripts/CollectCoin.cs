using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public static int collected = 0;
    public Text txt;

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.tag == "Coin")
        {
            collected++;
            Destroy(otherCollider.gameObject);
        }

        txt.text = "Pièces : " + collected.ToString();
    }
}

