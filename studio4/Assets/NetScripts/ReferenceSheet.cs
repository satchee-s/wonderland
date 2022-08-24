using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceSheet : MonoBehaviour
{
    public Button connectButton;
    public TMP_InputField playerNameInputField;
    public GameObject connectPanel;
    public GameObject matchMakingPanel;
    public GameObject Player2Panel;
    public GameObject TransitionPanel;

    public GameObject opponentFound;
    public GameObject lookingForOpponent;

    public Button startButton;
    public Button exitButton;
    public TextMeshProUGUI setPlayerName;
    public NetworkComponent nc;
    public PlayerManager playerManager;
}
