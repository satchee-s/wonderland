using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardListManager : MonoBehaviour
{
    public List<GameObject> CreaturecardList;
    public List<GameObject> BoostercardList;

    public List<GameObject> CreaturecardSlotsList;
    public List<GameObject> BoostercardSlotsList;

    public Card card;


    private Vector3 centerScreenPosition;

    private Camera mainCam;
    private GameObject goBackButton;

    private void Start()
    {
        centerScreenPosition =  mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -2f));
    }

    void Update()
    {
        for(int i = 0; i < CreaturecardList.Count; i++) //Creature
        {
            CreaturecardSlotsList[i].transform.position = CreaturecardList[i].transform.position;
        }

        for (int i = 0; i < BoostercardList.Count; i++) //Booster
        {
            BoostercardSlotsList[i].transform.position = BoostercardList[i].transform.position;
        }

    }

}
