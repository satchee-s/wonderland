using UnityEngine;
using core;

public class Slot : MonoBehaviour
{
    [SerializeField] private CardType slotType;
    public Card currentCard;
    public Slot correspondingSlot;
    [SerializeField] NetManager netManager;
    [SerializeField] PlayerManager playerManager;

    public bool HasCard => currentCard != null;
    public CardType Type => slotType;
    
    public void PlaceCardInSlot(Card card)
    {
        if(card.Type != slotType) return;
        
        currentCard = card;
        card.originalPos = transform.position;
        CardMouseInteraction.onDragEvent += RemoveCardFromSlot;
       // SendCardPacket(card);
        //playerManager.playedCards.Add(card);
    }

    private void RemoveCardFromSlot(CardMouseInteraction draggedCard)
    {
        if(currentCard.MouseInteraction != draggedCard) return;
        playerManager.playedCards.Remove(currentCard);
        print("Remove");
        currentCard = null;
        CardMouseInteraction.onDragEvent -= RemoveCardFromSlot;
        //netManager.DestroyObject(currentCard);
    }

    void SendCardPacket(Card card)
    {
        InstantiatePacket ip = new InstantiatePacket(card.name, card.name, card.transform.position, card.transform.rotation, netManager.player);
        netManager.SendPacket(ip.StartSerialization());
    }
}