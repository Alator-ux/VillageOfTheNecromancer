using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftController : MonoBehaviour
{
    [SerializeField]
    private List<Recipe> knownRecipes;
    private List<Recipe> availabelRecipes;

    private Inventory inventory;
    private void Start()
    {
        availabelRecipes = new List<Recipe>();

        if(inventory == null)
        {
            inventory = transform.GetComponent<Inventory>();
        }
    }
    public void AddRecipe(Recipe craftableItem)
    {
        knownRecipes.Add(craftableItem);
    }
    public void RemoveRecipe(Recipe craftableItem)
    {
        knownRecipes.Remove(craftableItem);
    }
    public Tuple<List<Recipe>, int> GetKnownCraftRecipes()
    {
        var itemToCount = inventory.GetItemToCount();
        /* availabelRecipes = knownRecipes.Where(recipe => recipe.Ingredients.All(ingredient =>
             itemToCount.ContainsKey(ingredient.item) && itemToCount[ingredient.item] >= ingredient.count)).ToList();*/
        var availabelRecipesCount = 0;
        knownRecipes = knownRecipes.OrderByDescending(recipe => {
            var availabel = recipe.Ingredients.All(ingredient =>
             itemToCount.ContainsKey(ingredient.item) && itemToCount[ingredient.item] >= ingredient.count);
            if (availabel)
            {
                availabelRecipesCount++;
            }
            return availabel;
        }
        ).ToList();
        availabelRecipes = knownRecipes.Take(availabelRecipesCount).ToList();
        return new Tuple<List<Recipe>, int>(knownRecipes, availabelRecipesCount);
    }
    public Recipe GetCraftRecipe(int ind)
    {
        if(ind < 0 || ind >= knownRecipes.Count)
        {
            return null;
        }
        return knownRecipes[ind];
    }
    public bool CanCraft(Recipe recipe)
    {
        var itemToCount = inventory.GetItemToCount();
        var canCraft = recipe.Ingredients.All(ingredient => 
            itemToCount.ContainsKey(ingredient.item) && itemToCount[ingredient.item] >= ingredient.count);
        return canCraft;
    }
    public void Craft(Recipe recipe)
    {
        // �� ������, ����� ��, �.�. �� ���� ����� �������� �������� ������ �� AvailableCrafts
        /*if (!CanCraft(craftableItem))
        {
            return;
        }*/
        foreach(var ingredient in recipe.Ingredients)
        {
            inventory.RemoveItem(ingredient.item, ingredient.count);
        }
        inventory.AddItem(recipe.CraftedItem, recipe.CraftedNumber);
    }
    public void Craft(int ind)
    {
        if(ind < 0 || ind >= availabelRecipes.Count)
        {
            return;
        }
        Craft(availabelRecipes.ElementAt(ind));
    }
}
