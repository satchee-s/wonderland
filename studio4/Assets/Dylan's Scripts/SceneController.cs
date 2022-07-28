using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SceneController : MonoBehaviour
{
    public TMP_InputField enterName;
    public GameObject AlertMessage;

    private void Start()
    {
        AlertMessage.SetActive(false);
    }
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
        if(enterName != null)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            AlertMessage.SetActive(true);
        }
        
    }
    public void Exit()
    {
        Application.Quit();
    }

}
