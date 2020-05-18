using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckpoint : MonoBehaviour
{
    private static Vector3 checkpoint = new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
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
