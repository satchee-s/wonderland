using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureManager : MonoBehaviour
{
    protected Card card;
    public List<Card> opponentCreatureCards; //get list of opponent cards
    public List<Card> playerCreatureCards;

    private void Start()
    {
        card = GetComponent<Card>();
    }
    public virtual void PlayCard(Card otherCard, SlotsManager slot)
    {
        //slot.hasCard = false;
        this.gameObject.SetActive(false);
    }
}
