using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ouvrir / Fermer le menu de Pause
/// </summary>
public class OpenMenuScript : MonoBehaviour
{
    public bool isPauseMenuOpen = false;
    // Texte de Pause
    public Text myPauseText;

    public bool isGameOverOpen;

    void Update(){
        isGameOverOpen = gameObject.GetComponent<GameOverScript>().isOpen;
        if(Input.GetKeyDown(KeyCode.Escape) && isPauseMenuOpen == false && isGameOverOpen == false){
            // Met le jeu en pause
            PauseGame();
            // Récupération du script GameOverScript
            var GameOverCanvas = FindObjectOfType<GameOverScript>();
            // Affichage des boutons et du texte de Pause
            GameOverCanvas.ShowButtons();
            myPauseText.enabled = true;
            isPauseMenuOpen = true;
        }else if(Input.GetKeyDown(KeyCode.Escape) && isPauseMenuOpen == true  && isGameOverOpen == false){
            // Reprends le jeu
            ContinueGame();
            // Récupération du script GameOverScript
            var GameOverCanvas = FindObjectOfType<GameOverScript>();
            // Affichage des boutons et du texte de Pause
            GameOverCanvas.HideButtons();
            myPauseText.enabled = false;
            isPauseMenuOpen = false;
        }
    }

    private void PauseGame(){
        Time.timeScale = 0;
    }

    private void ContinueGame(){
        Time.timeScale = 1;
    }
}