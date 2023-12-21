using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDInventoryCell : MonoBehaviour, 
    IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler, IDropHandler
{
    private CanvasGroup canvasGroup;
    private Image itemImage, background;
    private TextMeshProUGUI itemCount;
    private int row, column;
    public int Row { get => row; set => row = value; }
    public int Column { get => column; set => column = value; }

    private Action<int, int> onItemSwapped;
    public Action<int, int> OnItemSwapped { set => onItemSwapped = value; }

    private Action onItemRemoved;
    public Action OnItemRemoved { set => onItemRemoved = value; }

    private GameObject topLevelObject;
    public GameObject TopLevelObject { set => topLevelObject = value; }

    public bool dropped = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        background = GetComponent<Image>();

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
    public void SetActiveColor()
    {
        background.color = new Color(0.68f, 0.48f, 0.0f, 1f);
    }

    public void SetInactiveColor()
    {
        background.color = new Color(0.34f, 0.24f, 0f, 1f);
    }
    public bool IsEqual(HUDInventoryCell other)
    {
        return row == other.Row && column == other.Column;
    }
    // ----- Dragging section begin ----
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
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
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        var cell = eventData.pointerDrag.GetComponent<HUDInventoryCell>();
        if (cell == null)
        {
            return;
        }
        cell.dropped = true;
        onItemSwapped(cell.Row, cell.Column);
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
    // ----- Dragging section end ----
}
