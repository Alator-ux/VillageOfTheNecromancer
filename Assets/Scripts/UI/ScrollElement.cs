using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScrollElement <ContentType>: MonoBehaviour
{
    [SerializeField]
    private GameObject contentItemPrefab;
    private GameObject contentElement;
    protected List<GameObject> contentItems;

    void Awake()
    {
        contentElement = transform.Find("Content").gameObject;
        contentItems = new List<GameObject>();

        var scrollRect = GetComponent<ScrollRect>();
        scrollRect.scrollSensitivity = 20f;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
    }
    virtual protected void ResizeContentItemList(int newContentSize)
    {
        if(newContentSize < 0)
        {
            return;
        }
        var removeCount = contentItems.Count;
        for (var toRemove = newContentSize; toRemove < removeCount; toRemove++)
        {
            Destroy(contentItems[0]);
            contentItems.RemoveAt(0);
        }
        for (var toAdd = contentItems.Count; toAdd < newContentSize; toAdd++)
        {
            var item = Instantiate(contentItemPrefab, contentElement.transform);
            contentItems.Add(item);
        }        
    }
    protected abstract void FillItemsWithContent(List<ContentType> contentList);

    public void SetContent(List<ContentType> contentList)
    {
        ResizeContentItemList(contentList.Count);
        FillItemsWithContent(contentList);
    }
}
