using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanelScript : MonoBehaviour
{
    private CraftController craftController;
    [SerializeField]
    private GameObject recipesScrollElement, ingredientsScrollElement, craftedItemDescElement;

    private AvailableRecipesScroll recipesScrollScript;
    private RecipeIngredientsScroll ingredientsScrollScript;
    private CraftedItemDescriptionScript craftedItemDescScript;

    Action onDisableCallback;
    public Action OnDisableCallback { set => onDisableCallback = value; }

    void Start()
    {
        var player = GameObject.Find("Player");
        craftController = player.GetComponent<CraftController>();

        recipesScrollScript = recipesScrollElement.GetComponent<AvailableRecipesScroll>();
        recipesScrollScript.OnItemClickCallback = () => {
            UpdateDescription();
            UpdateIngredients();
        };

        ingredientsScrollScript = ingredientsScrollElement.GetComponent<RecipeIngredientsScroll>();

        craftedItemDescScript = craftedItemDescElement.GetComponent<CraftedItemDescriptionScript>();

        SetInactive();
    }
    void UpdateDescription()
    {
        var recipe = craftController.GetCraftRecipe(recipesScrollScript.SelectedItemIndex);
        craftedItemDescScript.UpdateInfo(recipe);
    }
    void UpdateIngredients()
    {
        var recipe = craftController.GetCraftRecipe(recipesScrollScript.SelectedItemIndex);
        if (recipe == null) {
            ingredientsScrollScript.SetContent(new List<Ingredient>());
            return;
        }
        ingredientsScrollScript.SetContent(recipe.Ingredients);
    }
    void UpdateRecipes()
    {
        recipesScrollScript.SetContent(craftController.GetAvailableCraftRecipes());
        UpdateDescription();
        UpdateIngredients();
    }
    public void SetActive()
    {
        UpdateRecipes();
        gameObject.SetActive(true);
    }
    public void SetInactive()
    {
        gameObject.SetActive(false);
        onDisableCallback();
    }
    
    public void OnCraftButtonClick()
    {
        craftController.Craft(recipesScrollScript.SelectedItemIndex);
        UpdateRecipes();
    }
    public void OnExitButtonClick()
    {
        SetInactive();
    }
}
