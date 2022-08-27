using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] GameObject turnText;
    [SerializeField] TextMeshProUGUI opponentHealth;


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
        if(currentStep == 0)
        {
            gameMatchPanel.SetActive(false);
            turnText.SetActive(false);
        }
        if (currentStep == 6) //face health
        {
            gameMatchPanel.SetActive(true);
        }
        if (currentStep == 13) //play cardgaurdsman
        {
            FindCard(guardsman);
        }
        if (currentStep == 14) //play dodo
        {
            FindCard(dodo);
        }
        if (currentStep == 16) //play corruption
        {
            FindCard(corruption);
        }
        if (currentStep == 17) //knight health
        {
            currentCard.gameObject.SetActive(false);
        }
        if (currentStep == 18) //play recovery
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
        if(currentStep == 22)
        {
            opponentHealth.text = "38";
        }
        if (currentStep == 27) //sacrificetext
        {
            PlayerTurnSystem.currentMana = 1;
            PlayerTurnSystem.maxMana = 1;
            turnObject.ChangePlayerManaText("1/1");
        }
        if (currentStep == 33) 
        {
            turnText.SetActive(true);
        }
        if (currentStep == 34)
        {
            turnText.SetActive(false);
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
