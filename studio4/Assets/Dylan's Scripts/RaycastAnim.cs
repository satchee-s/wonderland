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

    private bool isOn = true;

    public Transform slot1;
    public Transform slot2;
    public Transform slot3;
    public Transform slot4;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        EToInteract.enabled = false;
        RToReturn.enabled = false;
        
    }

    private void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 10))
        {
            if (raycasthit.collider.CompareTag("Selectable") && isOn == true)//check if raycast tag is Equal to selectable tag
            {
                EToInteract.enabled = true;
                Debug.Log(raycasthit.collider.gameObject.name); // Shows which card the mpuse is over

                if (Input.GetKey(KeyCode.E))
                {
                  isOn = false;
                  EToInteract.enabled = false;
                   
                  Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));  //take that specfic gameobj with the mouse hovering over it to do the stuff below
                  raycasthit.collider.gameObject.transform.position = centerPos;
                  RToReturn.enabled = true;
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                isOn = true;
                RToReturn.enabled = false;
                EToInteract.enabled = true;
               
                //Figure out how to Make object go back to original Place

            }


            if (raycasthit.collider.CompareTag("Table"))
            {
                Debug.Log("Hover card");
                EToInteract.enabled = false;
            }
        }
    }
}
