using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailableRecipesScroll : SelectableScrollElement<Recipe>
{
    protected override void FillItemsWithContent(List<Recipe> contentList)
    {
        for (var i = 0; i < contentItems.Count; i++)
        {
            var craftedItemImage = contentItems[i].transform.Find("Image").GetComponent<Image>();
            craftedItemImage.sprite = contentList[i].CraftedItem.Image;

            var craftedItemName = contentItems[i].transform.Find("Name").GetComponent<TextMeshProUGUI>();
            craftedItemName.text = contentList[i].CraftedItem.Name;
        }
    }
}