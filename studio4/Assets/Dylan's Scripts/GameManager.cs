using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public GameObject Hand;

    public GameObject CardSlots;
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

   /* Animator animator;
    [SerializeField] private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit,10))
        {
            transform.position = raycasthit.point;
            if (raycasthit.collider.CompareTag("Selectable"))
            {
                Debug.Log("Hover card");
                animator.SetTrigger("HoveringCardTrigger");
            }
        }
    }*/
    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for(int i = 0; i < availableCardSlots.Length; i++)
            {
                if(availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.gameObject.tag = "Selectable";
                    randCard.transform.position = cardSlots[i].transform.position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void ShuffleDeck()
    {
        if (deck.Count > 1) //check if there are more than 1 card in the deck of cards
        {
            for(int i = 0; i < deck.Count; i++)
            {
                container[0] = deck[i]; 
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = container[0];
            }
        }
    }
}
