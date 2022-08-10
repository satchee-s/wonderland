using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    //RaycastAnim rayCast;
    CardMouseInteraction rayCast;
    public GameObject[] popUps;
    public GameObject firstPopUp;

    public GameObject TurnObj;
    public GameObject discardButton;
    public GameObject DrawOneandOne;
    public GameObject DrawTwo;
    public GameObject Shuffle;
    public GameObject endTurn;
    public GameObject sacrifice;
    public GameObject AttackCard;
    public GameObject AttackFace;
    public GameObject endTutorial;
    public GameObject Error;


    private int i;
    private int countForInput;


    void Start()
    {
        countForInput = 0;
        TurnObj.SetActive(false);
        DrawOneandOne.SetActive(false);
        DrawTwo.SetActive(false);
        Shuffle.SetActive(false);
        discardButton.SetActive(false);
    }

    public void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (this.i == i)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (i == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 1)
        {

        }
        else if (i == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {

                i++;
            }
        }
        else if (i == 3)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 4)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 5)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 6)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 7)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {

                i++;
            }
        }
        else if (i == 8)
        {
            //if player places booster card
            //{ if(player clicks on Discard button)
            //destroy this card (that is being interacted with)
            //discardButton.SetActive(true);
            //i++;

            //else if(player palces creature card)
            //Error.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {

            }
        }
        else if (i == 9)
        {
            discardButton.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {


                i++;
            }
        }
        else if (i == 10)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 11)
        {
            //if(player places 2 Creature cards on the creature row)
            //{ //if player taps on 2 creature cards then sacrifice button should pop up!)} 
            //sacrifice.setActive(True)
            // IF sacrifice button is pressed then destroy both of those cards 
            //Increase Mana
            // { i++ }

            //else if(booster is placed, Error Message (This is Not a Creature Card!
            //Error.SetActive(true);


            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 12)
        {
            sacrifice.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 13)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 14)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 15)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 16)
        {
            endTurn.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 17)
        {
            endTurn.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 18) //if start pop up (Deactivate Opponents turn UI or Move it elsewhere)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 19)
        {
            DrawOneandOne.SetActive(true);
            DrawTwo.SetActive(true);


            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 20)
        {
            DrawOneandOne.SetActive(false);
            DrawTwo.SetActive(false);

            //if(2 creature cards have been played)
            //{  i++; }
            //else if(Booster card has been played)
            //{ activate error that says (Cant Place Booster card here)
            //Error.SetActive(true);


            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 21)
        {

            //if( 1 creature card has been tapped on
            // i++

            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 22)
        {

            AttackCard.SetActive(true);
            AttackFace.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 23)
        {

            AttackCard.SetActive(false);
            AttackFace.SetActive(false);

            //if(players taps on 1 enemy card)
            //deal damage & take damage
            //set i == 25

            //else if(player taps on face button)
            //{ i == 25 }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 24)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 25)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 26)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 27)
        {

            endTutorial.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
    }

    public void Next()
    {
        i++;
    }
}
