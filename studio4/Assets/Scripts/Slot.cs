using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private CardType slotType;
    public Card currentCard;
    public Slot correspondingSlot;

    public bool HasCard => currentCard != null;
    public CardType Type => slotType;
    
    public void PlaceCardInSlot(Card card)
    {
        if(card.Type != slotType) return;
        
        currentCard = card;
        card.originalPos = transform.position;
        CardMouseInteraction.onDragEvent += RemoveCardFromSlot;
    }

    private void RemoveCardFromSlot(CardMouseInteraction draggedCard)
    {
        if(currentCard.MouseInteraction != draggedCard) return;
        
        print("Remove");
        currentCard = null;
        CardMouseInteraction.onDragEvent -= RemoveCardFromSlot;
    }
}