using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "NextLevel")
        {
            Application.LoadLevel("Level2");
        }
        if(col.tag == "Finish")
        {
            Application.LoadLevel("Menu");
        }
    }
}
