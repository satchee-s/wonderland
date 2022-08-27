using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using core;
public class GameManager : MonoBehaviour
{
    public List<Card> creatureDeck = new List<Card>();
    public List<Card> boosterDeck = new List<Card>();
    public List<Card> container = new List<Card>();
    public List<Card> handCards = new List<Card>();
    public GameObject OneboosterOneCreature;
    public GameObject TwoCreature;
    public GameObject ShuffleCard;

    public Transform[] cardSlots;
    public bool[] availableCardSlots;//this shouldnt be here
    public PlayerSlotsManager player1Slots; //8 slots
    public PlayerSlotsManager player2Slots;//8 slots
    public bool HasDrawedCards = false;
    NetManager netManager;
    public void Start()
    {
        netManager = FindObjectOfType<NetManager>();
        SetRandomCards();
        StartCoroutine(SendCardData());
    }

    void SetRandomCards()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < 2)
            {
                handCards.Add(boosterDeck[Random.Range(0, boosterDeck.Count)]);
                handCards[i].tag = "Selectable";
                boosterDeck.Remove(handCards[i]);
                handCards[i].transform.position = cardSlots[i].transform.position;
                handCards[i].GetComponent<NetworkComponent>().GameObjectID = i;
            }
            else
            {
                handCards.Add(creatureDeck[Random.Range(0, creatureDeck.Count)]);
                handCards[i].tag = "Selectable";
                creatureDeck.Remove(handCards[i]);
                handCards[i].transform.position = cardSlots[i].transform.position;
                handCards[i].GetComponent<NetworkComponent>().GameObjectID = i;
            }

            //Card.NameOfCard card2 = (Card.NameOfCard)System.Enum.Parse(typeof(Card.NameOfCard), enumtostring);
            //PositionPacket pp = new PositionPacket(handCards[i].transform.position, netManager.player);
            //netManager.SendPacket(pp.StartSerialization());
        }
    }

    IEnumerator SendCardData()
    {
        yield return new WaitForSeconds(4);
        for (int i = 0; i < handCards.Count; i++)
        {
            netManager.GameManagerInstantiation(handCards[i].nameCard);
        }
        yield return null;
    }
}
