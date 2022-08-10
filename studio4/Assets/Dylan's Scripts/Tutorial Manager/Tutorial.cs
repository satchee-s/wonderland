using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> popups = new List<GameObject>();
    [SerializeField] PlayerSlotsManager slots;
    int currentStep = 0;
    [SerializeField] Card guardsman;
    [SerializeField] Card dodo;
    [SerializeField] Card corruption;
    [SerializeField] Card recovery;
    Card currentCard;

    [SerializeField] GameObject wrongSlot;

    private void Start()
    {
        popups[currentStep].SetActive(true);
    }

    private void Update()
    {
        if (currentStep == 7)
        {
            FindCard(guardsman);
        }
        if (currentStep == 8)
        {
            FindCard(dodo);
        }
        if (currentStep == 11)
        {
            FindCard(corruption);
        }
        if (currentStep == 12)
        {
            currentCard.gameObject.SetActive(false);
        }
        if (currentStep == 13)
        {
            for (int i = 0; i < 8; i++)
            {
                if (slots.allSlots[i].currentCard != null)
                {
                    if (slots.allSlots[i].currentCard.name == recovery.name)
                    {
                        if (slots.allSlots[i].correspondingSlot.currentCard != guardsman)
                        {
                            StartCoroutine(ErrorMessage(wrongSlot));
                        }
                        else
                        {
                            currentCard = slots.allSlots[i].currentCard;
                            NextStep();
                            break;
                        }
                    }
                }
            }
        }
    }

    public void NextStep()
    {
        popups[currentStep].SetActive(false);
        if (currentStep < popups.Count)
        {
            currentStep++;
            popups[currentStep].SetActive(true);
        }
    }

    void FindCard(Card cardNeeded)
    {
        for (int i = 0; i < 8; i++)
        {
            if (slots.allSlots[i].currentCard != null)
            {
                if (slots.allSlots[i].currentCard.name == cardNeeded.name)
                {
                    currentCard = slots.allSlots[i].currentCard;
                    NextStep();
                    Debug.Log(cardNeeded + " Played");
                    break;
                }
            }
        }
    }

    IEnumerator ErrorMessage(GameObject errorMessage)
    {
        errorMessage.SetActive(true);
        new WaitForSeconds(2);
        errorMessage.SetActive(false);
        yield return null;
    }
}
