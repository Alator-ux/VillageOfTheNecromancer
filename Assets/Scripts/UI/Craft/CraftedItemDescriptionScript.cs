using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftedItemDescriptionScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description, craftedNumber;
    public void Clear()
    {
        description.text = "";
        craftedNumber.text = "";
    }
    public void UpdateInfo(Recipe recipe)
    {
        if(recipe == null)
        {
            Clear();
            return;
        }

        description.text = recipe.CraftedItem.Description;
        craftedNumber.text = $"Будет создано {recipe.CraftedNumber} штук";
    }
}
