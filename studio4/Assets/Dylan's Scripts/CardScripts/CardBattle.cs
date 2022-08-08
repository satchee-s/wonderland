using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBattle : MonoBehaviour
{
    private Camera cam;
    private Card EnemyCard;
    private Card PlayerCard;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyCard != null)
        {
        Debug.Log(EnemyCard.transform.name);

        }
        if (PlayerCard != null)
        {
            Debug.Log(PlayerCard.transform.name);
        }
            
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray,out RaycastHit hit);

            if (hit.collider.TryGetComponent<Card>(out Card card))
            {
                if (card.isOpponentCard)
                {
                    EnemyCard = card;
                }
                else
                {
                    PlayerCard = card;
                }
            }
        }
    }
}
