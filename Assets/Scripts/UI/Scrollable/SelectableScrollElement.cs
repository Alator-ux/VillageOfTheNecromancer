using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

abstract public class SelectableScrollElement<ContentType> : ScrollElement<ContentType>
{
    protected Dictionary<string, int> idToIndex;
    protected List<SelectableObject> uniqueObjects;
    protected int selectedItemIndex;
    public int SelectedItemIndex
    {
        get
        {
            /*EventSystem eventSystem = EventSystem.current;
            var currentSelectable = eventSystem.currentSelectedGameObject?.GetComponent<SelectableObject>();
            selectedItemIndex = -1;
            if (currentSelectable != null && idToIndex.ContainsKey(currentSelectable.Id))
            {
                var prob = idToIndex[currentSelectable.Id];
                if (currentSelectable.FullEqual(uniqueObjects[prob]))
                {
                    selectedItemIndex = idToIndex[currentSelectable.Id];
                }
            }*/
            return selectedItemIndex;
        }
    }

    private Action onItemClickCallback;
    public Action OnItemClickCallback { set => onItemClickCallback = value; }
    protected void Start()
    {
        idToIndex = new Dictionary<string, int>();
        uniqueObjects = new List<SelectableObject>(0);
        selectedItemIndex = -1;
    }

    public override void SetContent(List<ContentType> contentList)
    {
        /*EventSystem eventSystem = EventSystem.current;
        var currentSelectable = eventSystem.currentSelectedGameObject?.GetComponent<SelectableObject>();

        bool selectedInScroll = false;
        if(currentSelectable != null && idToIndex.ContainsKey(currentSelectable.Id))
        {
            selectedInScroll = currentSelectable.FullEqual(uniqueObjects[idToIndex[currentSelectable.Id]]);
        }*/
        string selectedId = "";
        if (selectedItemIndex != -1)
        {
            selectedId = contentItems[selectedItemIndex].GetComponent<SelectableObject>().Id;
        }

        base.SetContent(contentList);

        int newContentSize = contentList.Count;

        idToIndex = new Dictionary<string, int>();
        uniqueObjects = new List<SelectableObject>(newContentSize);

        for (var i = 0; i < contentItems.Count; i++)
        {
            uniqueObjects.Add(contentItems[i].GetComponent<SelectableObject>());
            uniqueObjects[i].Id = contentList[i].GetHashCode().ToString();
            if(selectedId == uniqueObjects[i].Id) { 
                selectedItemIndex = i;
                EventSystem.current.SetSelectedGameObject(contentItems[selectedItemIndex]);
            }

            idToIndex[uniqueObjects[i].Id] = i;

            var itemButton = contentItems[i].GetComponent<Button>();
            SetCallbacks(itemButton, i);

        }

        /*if(selectedInScroll)
        {
            if (idToIndex.ContainsKey(currentSelectable.Id))
            {
                selectedItemIndex = idToIndex[currentSelectable.Id];
                eventSystem.SetSelectedGameObject(contentItems[selectedItemIndex]);
            }
            else
            {
                selectedItemIndex = -1;
                eventSystem.SetSelectedGameObject(null);
            }            
        }*/
    }
    public void RemoveSelection()
    {
        selectedItemIndex = -1;
        /*EventSystem eventSystem = EventSystem.current;
        var selectedObject = eventSystem.currentSelectedGameObject.GetComponent<SelectableObject>();
        if (selectedObject == null || !idToIndex.ContainsKey(selectedObject.Id))
        {
            return;
        }

        if (contentItems[idToIndex[selectedObject.Id]].GetComponent<SelectableObject>().FullEqual(selectedObject))
        {
            eventSystem.SetSelectedGameObject(null);
        }*/
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
    public void RecoverSelection()
    {
        if(selectedItemIndex != -1)
        {
            EventSystem.current.SetSelectedGameObject(contentItems[selectedItemIndex]);
        }
    }
}
