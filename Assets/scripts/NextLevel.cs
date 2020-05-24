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
            //On remet les checkpoints ramassés à zéro pour pouvoir rejouer sans problèmes de respawn
            CheckpointScript.setLastCheckpoint(new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity));
            
            Application.LoadLevel("Menu");
        }
    }
}
