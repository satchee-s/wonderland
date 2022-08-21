using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net.Sockets;
using core;

public class PlayerTurnSystem : MonoBehaviour
{
    public static bool isYourTurn, startTurn, endTurn;
    public int yourTurn, OponentsTurn, seconds, random;
    public TextMeshProUGUI turnText, manaText, timerText, enemyManaText;
    public static int maxMana, currentMana, maxEnemyMana, currentEnemyMana;
    public bool timerStart, manabool;
    [SerializeField] NetManager netManager;
    PlayerManager player1, player2; 
    
    void Start()
    {
        StartRound();
        seconds = 30;
        timerStart = true;
        player1 = netManager.playerManagers[0];
        player2 = netManager.playerManagers[1];
        netManager = FindObjectOfType<NetManager>();//only works when server is running
    }

    void Update()
    {
        if (isYourTurn == true)
        {
            turnText.text = "your Turn";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            turnText.text = "Oponent Turn";
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(manaText)
        manaText.text = currentMana + "/" + maxMana;

        if (isYourTurn == true && seconds > 0 && timerStart == true)
        {
            StartCoroutine(Timer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == true)
        {
            EndYourTurn();
            timerStart = true;
            seconds = 30;
        }
        timerText.text = seconds + "";

        if (isYourTurn == false && seconds > 0 && timerStart == true)
        {
            StartCoroutine(EnemyTimer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == false)
        {
            EndOponentsTurn();
            timerStart = true;
            seconds = 30;
        }
        enemyManaText.text = currentEnemyMana + "/" + maxEnemyMana;
    }
    public void EndYourTurn()
    {
        isYourTurn = false;
        OponentsTurn += 1;

        if (manabool == true)
        {
            maxEnemyMana += 2;
            currentEnemyMana += 2;
        }
        else
        {
            maxEnemyMana += 1;
            currentEnemyMana += 1;
        }

        timerStart = true;
        seconds = 30;
        //UpdatePlayerData();
    }

    public void EndOponentsTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        if (manabool == false)
        {
            maxMana += 2;
            currentMana += 2;
        }
        else
        {
            maxMana += 1;
            currentMana += 1;
        }

        startTurn = true;
        timerStart = true;
        seconds = 30;
    }

    public void StartRound()
    {
        random = Random.Range(0, 2);

        if (random == 0)
        {
            isYourTurn = true;
            manabool = true;

            yourTurn = 1;
            OponentsTurn = 0;

            maxMana = 1;
            currentMana = 1;

            maxEnemyMana = 0;
            currentEnemyMana = 0;

            startTurn = false;
        }

        if (random == 1)
        {
            isYourTurn = false;
            manabool = false;
            yourTurn = 0;
            OponentsTurn = 1;

            maxMana = 0;
            currentMana = 0;

            maxEnemyMana = 1;
            currentEnemyMana = 1;

        }
    }

    IEnumerator Timer()
    {
        if (isYourTurn == true && seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;

            StartCoroutine(Timer());
        }
    }


    IEnumerator EnemyTimer()
    {
        if (isYourTurn == false && seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;

            StartCoroutine(EnemyTimer());
        }
    }

    public void endTurnbutton()
    {
        if (isYourTurn == true)
        {
            EndYourTurn();
        }
    }

    void UpdatePlayerData()
    {
        PlayerDataPacket playerData = new PlayerDataPacket(netManager.player, player1.health, currentMana);
        byte[] buffer = playerData.StartSerialization();
        netManager.SendPacket(buffer);
        Debug.Log("Player data sent");
    }

    public void ChangeOpponentMana(int newMana)
    {
        currentEnemyMana = newMana;
    }


    public bool getTime()
    {
        Debug.Log("timer started" + timerStart);
        return this.timerStart;
    }
}
