using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Start or quit the game
/// </summary>
public class RespawnCanvasScript : MonoBehaviour
{
    // Texte de réapparition
    public Text myText;

    public void EnableText()
    {
        myText.enabled = true;
    }

    public void DisableText()
    {
        myText.enabled = false;
    }
}