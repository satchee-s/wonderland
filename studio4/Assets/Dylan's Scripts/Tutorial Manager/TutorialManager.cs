using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    RaycastAnim rayCast;
    public GameObject[] popUps;
    public GameObject firstPopUp;
    public GameObject TurnObj;
    public GameObject DrawOneandOne;  
    public GameObject DrawTwo;
    public GameObject Shuffle;
    private int i;
    private int countForInput;


    void Start()
    {
        countForInput = 0;
        TurnObj.SetActive(false);
        DrawOneandOne.SetActive(false);
        DrawTwo.SetActive(false);
        Shuffle.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 9)
        {
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 12)
        {
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 17)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 18)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 19)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 20)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 21)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 22)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
        else if (i == 23)
        {
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
