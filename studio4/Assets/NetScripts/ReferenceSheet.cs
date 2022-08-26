using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceSheet : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerManager enemyManager;
    public PlayerSlotsManager playerSlotsManager;
    public PlayerTurnSystem turnSystem;
    public InstantiateHandler instantiateHandler;
    NetManager netManager;


    private void Start()
    {
        netManager = FindObjectOfType<NetManager>();
        netManager.playerManager = playerManager;
        netManager.enemyManager = enemyManager;
        netManager.slotManager = playerSlotsManager;
        netManager.slotManager = playerSlotsManager;
        netManager.turnSystem = turnSystem;
        netManager.instantiateHandler = instantiateHandler;
    }
}
