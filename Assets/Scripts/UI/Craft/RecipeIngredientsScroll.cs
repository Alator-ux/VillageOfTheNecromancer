using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIngredientsScroll : ScrollElement<Ingredient>
{
    protected override void FillItemsWithContent(List<Ingredient> contentList)
    {
        for (var i = 0; i < contentItems.Count; i++)
        {
            var ingredientImage = contentItems[i].transform.Find("Image").GetComponent<Image>();
            ingredientImage.sprite = contentList[i].item.Image;

            var ingredientName = contentItems[i].transform.Find("Name").GetComponent<TextMeshProUGUI>();
            ingredientName.text = contentList[i].item.Name;

            var ingregientCount = contentItems[i].transform.Find("Count").GetComponent<TextMeshProUGUI>();
            ingregientCount.text = $"x{contentList[i].count}";
        }

    }
}
