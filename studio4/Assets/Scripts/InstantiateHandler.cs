using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHandler : MonoBehaviour
{
    [SerializeField] Transform[] cardSlots;
    [SerializeField] PlayerManager opponentManager;
    int id = 0;

    public void InstantiateOnEnemySide(string cardType)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i] != null)
            {
                FindCardType(cardType, cardSlots[i].position, cardSlots[i].rotation);
            }
        }
    }
    void FindCardType(string cardType, Vector3 position, Quaternion rotation)
    {
        Card.NameOfCard nameOfCard = (Card.NameOfCard)System.Enum.Parse(typeof(Card.NameOfCard), cardType);
        GameObject newCard;
        switch (nameOfCard)
        {
            case Card.NameOfCard.Hypnosis:
                newCard = Instantiate(Resources.Load("Prefabs/Hypnosis"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                id++;
                opponentManager.playedHandCards.Add(newCard);
                break;

            case Card.NameOfCard.Regeneration:
                newCard = Instantiate(Resources.Load("Prefabs/Regeneration"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                id++;
                opponentManager.playedHandCards.Add(newCard);
                break;

            case Card.NameOfCard.Recovery:
                newCard = Instantiate(Resources.Load("Prefabs/Swift Recovery"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                id++;
                opponentManager.playedHandCards.Add(newCard);
                break;

            case Card.NameOfCard.Choice:
                newCard = Instantiate(Resources.Load("Prefabs/The Skull's Choice"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                id++;
                opponentManager.playedHandCards.Add(newCard);
                break;

            case Card.NameOfCard.Corruption:
                newCard = Instantiate(Resources.Load("Prefabs/Corruption"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                id++;
                opponentManager.playedHandCards.Add(newCard);
                break;

            case Card.NameOfCard.Swap:
                newCard = Instantiate(Resources.Load("Prefabs/Creature Swap"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Executioner:
                newCard = Instantiate(Resources.Load("Prefabs/Executioner"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Cat:
                newCard = Instantiate(Resources.Load("Prefabs/Chesire Cat"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Hatter:
                newCard = Instantiate(Resources.Load("Prefabs/Mad Hatter"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Knight:
                newCard = Instantiate(Resources.Load("Prefabs/Red Knight"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Guardsman:
                newCard = Instantiate(Resources.Load("Prefabs/Card Guardsman"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Rabbit:
                newCard = Instantiate(Resources.Load("Prefabs/White Rabbit"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Twins:
                newCard = Instantiate(Resources.Load("Prefabs/The Twins"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            case Card.NameOfCard.Dodo:
                newCard = Instantiate(Resources.Load("Prefabs/Dod"), position, rotation) as GameObject;
                newCard.GetComponent<NetworkComponent>().GameObjectID = id;
                opponentManager.playedHandCards.Add(newCard);
                id++;
                break;

            default:
                break;
        }
    }
}
