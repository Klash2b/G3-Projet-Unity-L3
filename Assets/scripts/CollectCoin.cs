using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public Text txt;

    void Start()
    {
        txt.text = "PIECES : " + Collected.getCollected().ToString();
    }

    void Update()
    {
        txt.text = "PIECES : " + Collected.getCollected().ToString();
    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.tag == "Coin")
        {
            Collected.setCollected(Collected.getCollected()+1);
            Destroy(otherCollider.gameObject);
        }
    }
}

