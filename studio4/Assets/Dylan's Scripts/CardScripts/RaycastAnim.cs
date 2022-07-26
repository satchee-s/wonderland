using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaycastAnim : MonoBehaviour
{

    [SerializeField] private Camera mainCam;
    GameManager gm;
    public TextMeshProUGUI EToInteract;
    public TextMeshProUGUI RToReturn;
    public float lockPos;
    private bool isOn = true;
    GameObject[] SelectableCardList;


    private void Awake()
    {
        mainCam = Camera.main;
    }
    void Start()
    {
        EToInteract.enabled = false;
        RToReturn.enabled = false;

        SelectableCardList = GameObject.FindGameObjectsWithTag("Selectable");
    }

    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 100))
        {

            if (raycasthit.collider.gameObject.CompareTag("Selectable") && isOn == true)//check if raycast tag is Equal to selectable tag
            {
                EToInteract.enabled = true;
                //Debug.Log(raycasthit.collider.gameObject.name); // Shows which card the mpuse is over
                raycasthit.collider.gameObject.transform.rotation = Quaternion.Euler(5f, lockPos, lockPos);


                if (Input.GetKey(KeyCode.E))
                {

                    foreach (GameObject card in SelectableCardList)
                    {
                        card.gameObject.tag = "InteractingCard";
                    }

                    isOn = true;
                    EToInteract.enabled = false;
                    RToReturn.enabled = true;

                    Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));  //take that specfic gameobj with the mouse hovering over it to do the stuff below
                    raycasthit.collider.gameObject.transform.position = centerPos;
                }
            }
            if (Input.GetKey(KeyCode.R))
            {

                foreach (GameObject card in SelectableCardList)
                {
                    card.gameObject.tag = "Selectable";
                }

                isOn = true;
                RToReturn.enabled = false;
                EToInteract.enabled = true;

                gameObject.transform.position = gameObject.GetComponent<Card>().originalPos;
            }

            if (raycasthit.collider.CompareTag("Table"))
            {
                gameObject.transform.rotation = gameObject.GetComponent<Card>().originalRotationValue;
                EToInteract.enabled = false;
            }
        }
    }
}
