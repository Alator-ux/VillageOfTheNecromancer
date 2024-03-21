using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftPanelScript : MonoBehaviour
{
    private CraftController craftController;
    [SerializeField]
    private GameObject recipesScrollElement, ingredientsScrollElement, craftedItemDescElement;
    [SerializeField]
    private GameObject craftButtonObject;

    private KnownRecipesScroll recipesScrollScript;
    private RecipeIngredientsScroll ingredientsScrollScript;
    private CraftedItemDescriptionScript craftedItemDescScript;
    private Button craftButton;

    Action onDisableCallback;
    public Action OnDisableCallback { set => onDisableCallback = value; }

    void Start()
    {
        var player = GameObject.Find("Player");
        craftController = player.GetComponent<CraftController>();

        recipesScrollScript = recipesScrollElement.GetComponent<KnownRecipesScroll>();
        recipesScrollScript.OnItemClickCallback = () => {
            UpdateDescription();
            UpdateIngredients();
            UpdateCraftButton();
        };

        ingredientsScrollScript = ingredientsScrollElement.GetComponent<RecipeIngredientsScroll>();

        craftedItemDescScript = craftedItemDescElement.GetComponent<CraftedItemDescriptionScript>();

        craftButton = craftButtonObject.GetComponent<Button>();
        craftButton.interactable = false;

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
    void UpdateCraftButton()
    {
        craftButton.interactable = recipesScrollScript.IsSelectedRecipeAvailable;
    }
    void UpdateRecipes()
    {
        var recipesNAvailableCount = craftController.GetKnownCraftRecipes();
        recipesScrollScript.SetContent(recipesNAvailableCount.Item1);
        recipesScrollScript.SetAvailableCount(recipesNAvailableCount.Item2);
        
        UpdateDescription();
        UpdateIngredients();
        UpdateCraftButton();
    }
    public void SetActive()
    {
        recipesScrollScript.RemoveSelection();
        EventSystem.current.SetSelectedGameObject(null);
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
        recipesScrollScript.RecoverSelection();
        UpdateRecipes();
    }
    public void OnExitButtonClick()
    {
        SetInactive();
    }
}
