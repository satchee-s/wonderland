using UnityEngine;
using core;

public class Slot : MonoBehaviour
{
    [SerializeField] private CardType slotType;
    public Card currentCard;
    public Slot correspondingSlot;
    //[SerializeField] NetManager netManager;
    //[SerializeField] PlayerManager playerManager;

    public bool HasCard => currentCard != null;
    public CardType Type => slotType;
    
    public void PlaceCardInSlot(Card card)
    {
        if(card.type != slotType) return;
        
        currentCard = card;
        card.originalPos = transform.position;
        CardMouseInteraction.onDragEvent += RemoveCardFromSlot;
        //InstantiatePacket ip = new InstantiatePacket(card.name, card.transform.position, card.transform.rotation);
        //netManager.SendPacket(ip.StartSerialization());
        //Debug.Log("Instantiation packet sent");
        //CardPacket cp = new CardPacket(card.cardId, card.name, card.health, card.attack, card.sleep);
        //netManager.SendPacket(cp.StartSerialization());
        //playerManager.playedCards.Add(card);
    }

    private void RemoveCardFromSlot(CardMouseInteraction draggedCard)
    {
        if(currentCard.MouseInteraction != draggedCard) return;
        //playerManager.playedCards.Remove(currentCard);
        print("Remove");
        //DestroyPacket dp = new DestroyPacket(currentCard.GetInstanceID());
        //netManager.SendPacket(dp.StartSerialization());
        currentCard = null;
        CardMouseInteraction.onDragEvent -= RemoveCardFromSlot;
    }
}