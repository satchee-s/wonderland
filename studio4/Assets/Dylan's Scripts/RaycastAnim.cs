using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAnim : MonoBehaviour
{
    Animator animator;
    [SerializeField] private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, 10))
        {
            if (raycasthit.collider.CompareTag("Selectable"))
            {
                Debug.Log("Hover card");
                animator.SetTrigger("HoveringCardTrigger");
            }
        }
    }
}
