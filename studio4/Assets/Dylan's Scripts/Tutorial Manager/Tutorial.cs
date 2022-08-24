using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> popups = new List<GameObject>();
    [SerializeField] PlayerSlotsManager slots;
    int currentStep = 0;
    [SerializeField] Card.NameOfCard guardsman;
    [SerializeField] Card.NameOfCard dodo;
    [SerializeField] Card.NameOfCard corruption;
    [SerializeField] Card.NameOfCard recovery;
    [SerializeField] GameObject gameMatchPanel;
    Card currentCard;

    [SerializeField] GameObject wrongSlot;
    [SerializeField] PlayerTurnSystem turnObject;
    [SerializeField] TutorialGameManager manager;

    private void Start()
    {
        popups[currentStep].SetActive(true);
    }

    private void Update()
    {
        if (currentStep == 5)
        {
            gameMatchPanel.SetActive(true);
        }
        if (currentStep == 9)
        {
            FindCard(guardsman);
        }
        if (currentStep == 10)
        {
            FindCard(dodo);
        }
        if (currentStep == 12)
        {
            FindCard(corruption);
        }
        if (currentStep == 13)
        {
            currentCard.gameObject.SetActive(false);
        }
        if (currentStep == 14)
        {
            for (int i = 0; i < 8; i++)
            {
                if (slots.allSlots[i].currentCard != null)
                {
                    if (slots.allSlots[i].currentCard.nameCard == recovery)
                    {
                        if (slots.allSlots[i].correspondingSlot.currentCard.nameCard != guardsman)
                        {
                            StartCoroutine(ErrorMessage(wrongSlot));
                            Debug.Log("Wrong slot used");
                        }
                        else
                        {
                            Debug.Log("Guardsman found in corresponding slot");
                            currentCard = slots.allSlots[i].currentCard;
                            NextStep();
                            break;
                        }
                    }
                }
            }
        }
        if (currentStep == 20)
        {
            PlayerTurnSystem.currentMana = 1;
            PlayerTurnSystem.maxMana = 1;
            turnObject.ChangePlayerManaText("1/1");
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

    void FindCard(Card.NameOfCard cardNeeded)
    {
        for (int i = 0; i < 8; i++)
        {
            if (slots.allSlots[i].currentCard != null)
            {
                if (slots.allSlots[i].currentCard.nameCard == cardNeeded)
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
