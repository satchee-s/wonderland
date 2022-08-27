using UnityEngine;
using core;

public class Slot : MonoBehaviour
{
    [SerializeField] private CardType slotType;
    public Card currentCard;
    public Slot correspondingSlot;

    //PlayerManager playerManager;
    NetManager netManager;
    [SerializeField] PlayerManager playerManager;

    public bool HasCard => currentCard != null;
    public CardType Type => slotType;
    
    public void PlaceCardInSlot(Card card)
    {
        if(card.type != slotType) return;

        playerManager.playedCards.Add(card);
        currentCard = card;
        card.originalPos = transform.position;
        CardMouseInteraction.onDragEvent += RemoveCardFromSlot;
        int id = card.GetComponent<NetworkComponent>().GameObjectID;
        netManager.SendPositionPacket(id, card.transform.position);
        netManager.DealDamage(id, card.attack);
    }

    private void RemoveCardFromSlot(CardMouseInteraction draggedCard)
    {
        if(currentCard.MouseInteraction != draggedCard) return;

        playerManager.playedCards.Remove(currentCard);
        print("Remove");
        currentCard = null;
        CardMouseInteraction.onDragEvent -= RemoveCardFromSlot;
    }

    private void Start()
    {
        netManager = FindObjectOfType<NetManager>();
    }
}