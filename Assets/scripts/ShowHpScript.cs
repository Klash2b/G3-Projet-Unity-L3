using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHpScript : MonoBehaviour
{
    public HealthScript player;
    public Text texte;

    void OnCollisionEnter2D (Collision2D col)
    {
        texte.text = "HP : " + player.hp;
    }

}

