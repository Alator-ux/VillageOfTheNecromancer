using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDInventoryCell : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler, IDropHandler
{
    private CanvasGroup canvasGroup;
    private Image itemImage;
    private TextMeshProUGUI itemCount;
    static private GameObject itemOptionsWindow;
    private int row, col;
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }

    private Action<int, int> onItemSwapped;
    public Action<int, int> OnItemSwapped { set => onItemSwapped = value; }

    private Action onItemRemoved;
    public Action OnItemRemoved { set => onItemRemoved = value; }

    private Action onAllItemsRemoved;
    public Action OnAllItemsRemoved { set => onAllItemsRemoved = value; }

    private Action onItemUsed;
    public Action OnItemUsed { set => onItemUsed = value; }

    [SerializeField]
    private GameObject itemOptionsWindowPrefab;

    private GameObject topLevelObject;
    public GameObject TopLevelObject { set => topLevelObject = value; }
    
    private bool IsEmpty { get => itemImage.sprite == null;}

    public bool dropped = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        var cellItem = transform.Find("Foreground");
        itemImage = cellItem.Find("ItemImage").GetComponent<Image>();
        itemCount = cellItem.Find("ItemCount").GetComponent<TextMeshProUGUI>();
    }
    public void SetItemStack(ItemStack itemStack)
    {
        if (itemStack == null)
        {
            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0);
            itemCount.text = "";
            return;
        }
        itemImage.sprite = itemStack.Item.Image;
        itemImage.color = Color.white;
        itemCount.text = itemStack.Count.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Destroy(itemOptionsWindow);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            topLevelObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            topLevelObject.transform.position = transform.position;
            itemImage.transform.SetParent(topLevelObject.transform);
            itemCount.transform.SetParent(topLevelObject.transform);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            itemImage.rectTransform.anchoredPosition += eventData.delta;
            itemCount.rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.blocksRaycasts = true;
            itemImage.transform.SetParent(transform);
            itemCount.transform.SetParent(transform);
            itemImage.rectTransform.anchoredPosition = new Vector2(0f, 0f);
            itemCount.rectTransform.anchoredPosition = new Vector2(0f, 0f);
            if (!dropped)
            {
                Debug.Log("dropped");
                onItemRemoved();
            }
            dropped = false;
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = true;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(itemOptionsWindow);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            itemOptionsWindow = Instantiate(itemOptionsWindowPrefab, topLevelObject.transform);
            itemOptionsWindow.transform.position = transform.position + new Vector3(60, 0, 0);
            var topLevelCanvasGroup = topLevelObject.GetComponent<CanvasGroup>();
            topLevelCanvasGroup.blocksRaycasts = true;
            itemOptionsWindow.GetComponent<HUDItemOptions>()
                .SetOnButtonClickCallback(delegate (int code)
                {
                    switch (code)
                    {
                        case 0:
                            onItemUsed();
                            break;
                        case 1:
                            onItemRemoved();
                            break;
                        case 2:
                            onAllItemsRemoved();
                            break;
                    }
                    topLevelCanvasGroup.blocksRaycasts = false;
                });
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            return;
        }
        var cell = eventData.pointerDrag.GetComponent<HUDInventoryCell>();
        if (cell == null)
        {
            return;
        }
        cell.dropped = true;
        onItemSwapped(cell.Row, cell.Col);
    }
}
