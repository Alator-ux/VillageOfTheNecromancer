using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnownRecipesScroll : SelectableScrollElement<Recipe>
{
    private int availableCount = 0;
    public bool IsSelectedRecipeAvailable
    { 
        get => selectedItemIndex != -1 && selectedItemIndex < availableCount; 
    }

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

    public void SetAvailableCount(int count)
    {
        availableCount = count;
        for (var i = 0; i < count; i++)
        {
            var button = contentItems[i].GetComponent<Button>();
            var colors = button.colors;
            colors.normalColor = new Color(1f, 1f, 1f);
            colors.highlightedColor = new Color(1f, 1f, 1f);
            colors.selectedColor = new Color(0.86f, 0.78f, 0.67f);
            colors.pressedColor = colors.selectedColor;
            button.colors = colors;
        }
        for (var i = count; i < contentItems.Count; i++)
        {
            var button = contentItems[i].GetComponent<Button>();
            var colors = button.colors;
            colors.normalColor = new Color(0.70f, 0.70f, 0.70f);
            /*colors.highlightedColor = new Color(0.65f, 0.65f, 0.65f);
            colors.selectedColor = new Color(0.61f, 0.54f, 0.45f);*/
            colors.highlightedColor = new Color(0.482f, 0.482f, 0.482f);
            colors.selectedColor = colors.highlightedColor;
            colors.pressedColor = colors.selectedColor;
            button.colors = colors;
        }
    }
}