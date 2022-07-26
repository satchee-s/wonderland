using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SceneController : MonoBehaviour
{

    public Button searchMatchButton;
    public Button tutorial;

    public Button exitGame;

    public void Awake()
    {
        MainMenu();
    }

   /* public void Update()
    {
        if (searchMatchButton)
        {
            connectToLobby();
        }

        if (tutorial)
        {
            TutorialScene();
        }

        if (exitGame)
        {
            Exit();
        }
        
    }*/
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void connectToLobby()
    {
        SceneManager.LoadScene("connectToLobby");
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Exit()
    {
        Application.Quit();
    }

}
