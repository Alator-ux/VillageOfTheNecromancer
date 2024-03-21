using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private List<Ingredient> ingredients = new List<Ingredient>();
    [SerializeField]
    private int craftedNumber = 1;
    [SerializeField]
    private Item craftedItem;
    public List<Ingredient> Ingredients { get => ingredients; }
    public int CraftedNumber { get => craftedNumber;
        set
        {
            craftedNumber = Mathf.Clamp(value, 1, 99);
        }
    }
    public Item CraftedItem { get => craftedItem; set => craftedItem = value; }
    private bool Contains(Ingredient otherIngredient)
    {
        int ind = 0;
        return Contains(otherIngredient, ref ind);
    }
    private bool Contains(Ingredient otherIngredient, ref int ind)
    {
        if (otherIngredient.item == null)
        {
            return false;
        }
        for (var i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].item == null)
            {
                continue;
            }
            if (ingredients[i].item.Name == otherIngredient.item.Name)
            {
                ind = i;
                return true;
            }
        }
        return false;
    }
    public void AddIngredient(Ingredient ingredient)
    {
        if (Contains(ingredient))
        {
            return;
        }
        ingredients.Add(ingredient);
    }
    public void AddIngredient(Item item, int count)
    {
        AddIngredient(new Ingredient(item, count));
    }
    public void RemoveIngredient(int ind)
    {
        if (ind < 0 || ind >= ingredients.Count)
        {
            return;
        }
        ingredients.RemoveAt(ind);
    }
    public void ReplaceIngredientAt(int ind, Ingredient ingredient)
    {
        if (ind < 0 || ind >= ingredients.Count)
        {
            return;
        }
        int cInd = 0;
        if (Contains(ingredient, ref cInd) && ind != cInd)
        {
            return;
        }
        ingredient.count = Mathf.Clamp(ingredient.count, 1, 99);
        ingredients[ind] = ingredient;
    }
    public void ReplaceIngredientAt(int ind, Item item, int count)
    {
        ReplaceIngredientAt(ind, new Ingredient(item, count));
    }
    public Ingredient GetIngredientAt(int ind)
    {
        if (ind < 0 || ind >= ingredients.Count)
        {
            return new Ingredient(null, 0);
        }
        return ingredients[ind];
    }
}


