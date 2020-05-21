using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Rejouer ou Retourner au menu principal
/// </summary>
public class GameOverScript : MonoBehaviour
{
    // Boutons
    private Button[] buttons;
    // Texte de Game Over
    public Text myText;

    // Booléen pour savoir si le menu est ouvert ou non
    // static pour que OpenMenuScript puisse bloquer l'ouverture du
    // menu de pause lorsque le menu de GameOver est déjà ouvert
    public bool isOpen = false;

    void Awake()
    {
        // Récupérer les boutons
        buttons = GetComponentsInChildren<Button>();

        // Désactiver les boutons et le texte par défaut
        HideButtons();
        DisableText();
    }

    // Permet de cacher les boutons
    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    // Permet d'afficher les boutons
    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    // Permet de retourner au menu principal
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Fonction qui vérifie quelle scène est actuellement jouée pour savoir
    // laquelle doit être rejouée au clic sur le bouton de Restart
    public void RestartGame()
    {
        // Créer une référence de la scène courante
        Scene currentScene = SceneManager.GetActiveScene();
     
        // Récupérer le nom de la scène courante
        string sceneName = currentScene.name;

        if (sceneName == "Level1") 
        {
            SceneManager.LoadScene("Level1");
        }
        else if (sceneName == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void EnableText()
    {
        Time.timeScale = 0;
        myText.enabled = true;
        // On met isOpen à true si on affiche les boutons
        isOpen = true;
    }

    public void DisableText()
    {
        Time.timeScale = 1;
        myText.enabled = false;
        // On met isOpen à false si on cache le texte
        isOpen = false;
    }
}