using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

abstract public class SelectableScrollElement<ContentType> : ScrollElement<ContentType>
{
    private int selectedItemIndex;
    public int SelectedItemIndex { get => selectedItemIndex; }

    private Action onItemClickCallback;
    public Action OnItemClickCallback { set => onItemClickCallback = value; }
    private void Start()
    {
        selectedItemIndex = -1;
    }

    protected override void ResizeContentItemList(int newContentSize)
    {
        base.ResizeContentItemList(newContentSize);
        if (selectedItemIndex >= newContentSize)
        {
            selectedItemIndex = -1;
        }
        if (selectedItemIndex != -1)
        {
            EventSystem eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(contentItems[selectedItemIndex]);
        }
        for(var i = 0; i < contentItems.Count; i++)
        {
            var itemButton = contentItems[i].GetComponent<Button>();
            SetCallbacks(itemButton, i);
        }
    }

    void SetCallbacks(Button button, int ind)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            selectedItemIndex = ind;
        });
        if (onItemClickCallback != null)
        {
            button.onClick.AddListener(() => { onItemClickCallback(); });
        }
    }
}
