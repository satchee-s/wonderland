using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaycastAnim : MonoBehaviour
{

    [SerializeField] private Camera mainCam;

    GameManager gm;
    public GameObject interactPanel;
    public GameObject goBackButton;

    public float lockPos;
    public  bool isOn = true;
    public GameObject[] SelectableCardList;

    public  void Awake()
    {
        mainCam = Camera.main;
        goBackButton.SetActive(false);

    }
    void Start()
    {
        SelectableCardList = GameObject.FindGameObjectsWithTag("Selectable");
        interactPanel.SetActive(false);
       
    }

    public void Update()
    {

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 100))
        {

            if (raycasthit.collider.gameObject.CompareTag("Selectable") && isOn == true)//check if raycast tag is Equal to selectable tag
            {
                interactPanel.SetActive(true);
                //Debug.Log(raycasthit.collider.gameObject.name); // Shows which card the mpuse is over
                raycasthit.collider.gameObject.transform.rotation = Quaternion.Euler(5f, lockPos, lockPos);

                if (Input.GetMouseButtonDown(0))
                {
                    
                    foreach (GameObject card in SelectableCardList)
                    {
                        card.gameObject.tag = "InteractingCard";
                    }
                  
                    isOn = true;
                    interactPanel.SetActive(false);
                    goBackButton.SetActive(true);

                    Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));  //take that specfic gameobj with the mouse hovering over it to do the stuff below
                    raycasthit.collider.gameObject.transform.position = centerPos;

                }

                
            }
           /* if (Input.GetKey(KeyCode.R))
            {

                foreach (GameObject card in SelectableCardList)
                {
                    card.gameObject.tag = "Selectable";
                }

                isOn = true;
                interactPanel.SetActive(true);

                gameObject.transform.position = gameObject.GetComponent<Card>().originalPos;
            } */

            if (raycasthit.collider.CompareTag("Table"))
            {

                interactPanel.SetActive(false);
                gameObject.transform.rotation = gameObject.GetComponent<Card>().originalRotationValue;
           
            }
        }
    }

    public void GoBack1()
    {
        foreach (GameObject card in SelectableCardList)
        {
            card.gameObject.tag = "Selectable";
        }

        isOn = true;
        interactPanel.SetActive(true);
        goBackButton.gameObject.SetActive(false);

        this.gameObject.transform.position = this.gameObject.GetComponent<Card>().originalPos;
    }
}
