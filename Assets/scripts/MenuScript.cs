using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    private static string difficulty = "normal";
    public void StartGame()
    {
        // "Stage1" is the name of the first scene we created.
        //Application.LoadLevel("Stage1");
        SceneManager.LoadScene("Level1");
        Collected.setCollected(0);
    }

    public void easy()
    {
        difficulty = "easy";
        GameObject.Find("Button Easy").GetComponent<Button>().interactable = false;
        GameObject.Find("Button Normal").GetComponent<Button>().interactable = true;
        GameObject.Find("Button Hard").GetComponent<Button>().interactable = true;
    }

    public void normal()
    {
        difficulty = "normal";
        GameObject.Find("Button Easy").GetComponent<Button>().interactable = true;
        GameObject.Find("Button Normal").GetComponent<Button>().interactable = false;
        GameObject.Find("Button Hard").GetComponent<Button>().interactable = true;
    }

    public void hard()
    {
        difficulty = "hard";
        GameObject.Find("Button Easy").GetComponent<Button>().interactable = true;
        GameObject.Find("Button Normal").GetComponent<Button>().interactable = true;
        GameObject.Find("Button Hard").GetComponent<Button>().interactable = false;
    }

    public static string getDifficulty()
    {
        return difficulty;
    }



}