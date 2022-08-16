using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMatchPanel : MonoBehaviour
{
   PlayerTurnSystem playerTurnSystem;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI Player1MagicHatCount;
    public TextMeshProUGUI Player2MagicHatCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTurnSystem.timerText.text = timerText.text;
        playerTurnSystem.manaText.text = Player1MagicHatCount.text;
        playerTurnSystem.enemyManaText.text = Player2MagicHatCount.text;
    }
}
