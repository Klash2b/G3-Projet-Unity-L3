using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private static Vector3 checkpoint = new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);

    void Start()
    {
        if (gameObject.tag == "CheckpointEasy" 
        && MenuScript.getDifficulty() != "easy")
        
        {
            gameObject.SetActive(false);
        }

        if (gameObject.tag == "CheckpointNormal" 
        && MenuScript.getDifficulty() == "hard")
        
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player" && !col.gameObject.GetComponent<Animator>().GetBool("isDying"))
        {
            checkpoint = transform.position;
            Destroy(gameObject);
        }
    }

    public static Vector3 getLastCheckpoint()
    {
        return checkpoint;
    }

    public static void setLastCheckpoint(Vector3 v)
    {
        checkpoint = v;
    }
}
