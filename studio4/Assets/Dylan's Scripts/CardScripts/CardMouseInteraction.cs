using System;
using UnityEngine;
using UnityEngine.UI;

public class CardMouseInteraction : MonoBehaviour
{
    private Camera mainCam;

    private float yDistFromTable;
    public GameObject interactPanel;
    public GameObject goBackButton;
    public static event Action<CardMouseInteraction> onDragEvent;
    public float lockPos;
    public GameObject[] SelectableCardList;
    public Card card;
    private Vector3 centerScreenPosition;
    private bool isZoomedIn;
    private static bool isDragged;
    public Vector3 DragPosition { get; private set; }

    public event Action onRelease;

    public void Awake()
    {
        mainCam = Camera.main;
        if (goBackButton != null)
            goBackButton.SetActive(false);
        card = GetComponent<Card>();
    }
    void Start()
    {
        centerScreenPosition = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        SelectableCardList = GameObject.FindGameObjectsWithTag("Selectable");
        if (interactPanel != null)
            interactPanel.SetActive(false);
        yDistFromTable = transform.position.y + 0.2f;
        if (card.isOpponentCard)
        {
            Destroy(this);
        }
    }

    private void OnMouseDown()
    {
        print("On Drag Event");
        onDragEvent?.Invoke(this);
        isDragged = true;
    }

    private void OnMouseEnter()
    {
        if (!isDragged)
        {
            transform.rotation = Quaternion.Euler(5f, lockPos, lockPos);
            interactPanel.SetActive(true);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isZoomedIn && !isDragged)
        {
            ZoomInOnCard();
        }
    }

    private void OnMouseExit()
    {
        if (!isZoomedIn)
        {
            ResetRotation();
            interactPanel.SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        if (gameObject.CompareTag("Selectable"))
            MoveAlongMouse();
    }

    private void OnMouseUp()
    {
        if (gameObject.CompareTag("Selectable"))
        {
            onRelease?.Invoke();
            transform.position = card.originalPos;
            isDragged = false;
        }
    }

    private void ZoomInOnCard()
    {
        isZoomedIn = true;
        foreach (GameObject card in SelectableCardList)
            card.gameObject.tag = "InteractingCard";

        interactPanel.SetActive(false);
        goBackButton.SetActive(true);
        goBackButton.GetComponent<Button>().onClick.AddListener(ZoomOutOfCard);

        MoveCardToCenter();
        transform.rotation = Quaternion.Euler(25f, 0f, 0f);
    }
    private void ZoomOutOfCard()
    {
        isZoomedIn = false;
        foreach (GameObject card in SelectableCardList)
            card.gameObject.tag = "Selectable";

        ResetRotation();
        goBackButton.GetComponent<Button>().onClick.RemoveAllListeners();
        goBackButton.SetActive(false);
        interactPanel.SetActive(true);
        gameObject.transform.position = card.originalPos;
    }

    private void MoveAlongMouse()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.WorldToScreenPoint(transform.position).z);
        Vector3 newPos = mainCam.ScreenToWorldPoint(screenPos);
        Vector3 pos = new Vector3(newPos.x, yDistFromTable, newPos.z);
        transform.position = pos;
        DragPosition = pos;
    }

    private void MoveCardToCenter()
    {
        Vector3 centerPos = centerScreenPosition;
        transform.position = centerPos;
    }
    private void ResetRotation() =>
        gameObject.transform.rotation = card.originalRotationValue;
}
