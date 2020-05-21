using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Annonce de réapparition
/// </summary>
public class RespawnCanvasScript : MonoBehaviour
{
    // Texte de réapparition
    public Text myText;

    void Awake()
    {
        // Désactive le texte par défaut
        DisableText();
    }


    public void EnableText()
    {
        myText.enabled = true;
    }

    public void DisableText()
    {
        myText.enabled = false;
    }
}