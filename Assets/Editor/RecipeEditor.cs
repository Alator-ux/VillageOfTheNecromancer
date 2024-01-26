using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
    private ReorderableList ingredientsList;
    private Recipe recipeData;
    private float listHorizontalItemSpacing = 10;
    private float listVerticalItemSpacing = 5;
    private float listObjectFieldWidth = 150;
    private float listRemoveButtonSize = 20;
    private float listCountTextfieldWidth = 40;
    public override bool HasPreviewGUI() => true;
    private void OnEnable()
    {
        recipeData = (Recipe)target;
        ingredientsList = new ReorderableList(recipeData.Ingredients, typeof(Ingredient), true, true, false, false);
        ingredientsList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            if (recipeData == null)
            {
                return;
            }
            rect.y += 2;
            var startX = rect.x;

            var ingredient = recipeData.GetIngredientAt(index);

            if (GUI.Button(new Rect(rect.x, rect.y, listRemoveButtonSize, EditorGUIUtility.singleLineHeight), "-"))
            {
                recipeData.RemoveIngredient(index);
                return;
            }

            rect.x += listRemoveButtonSize;
            rect.x += listHorizontalItemSpacing;

            var curItem = EditorGUI.ObjectField(new Rect(rect.x, rect.y, listObjectFieldWidth, EditorGUIUtility.singleLineHeight),
                "", recipeData.GetIngredientAt(index).item, typeof(Item), true) as Item;

            rect.x += listObjectFieldWidth;
            rect.x += listHorizontalItemSpacing;

            if (ingredient.item != null)
            {
                var imageSize = EditorGUIUtility.singleLineHeight * 3f - 2;
                

                EditorGUI.DrawPreviewTexture(new Rect(rect.x, rect.y, imageSize, imageSize),
                    ingredient.item.Image.texture);
            }

            rect.y += EditorGUIUtility.singleLineHeight;
            rect.y += listVerticalItemSpacing;
            rect.x = startX + listRemoveButtonSize + listHorizontalItemSpacing;

            var curCount = EditorGUI.IntField(new Rect(rect.x, rect.y, listCountTextfieldWidth, EditorGUIUtility.singleLineHeight),
                ingredient.count);
            
            recipeData.ReplaceIngredientAt(index, curItem, curCount);

            rect.x += listCountTextfieldWidth;
            rect.x += listHorizontalItemSpacing;


            var ingredientName = "None";
            if (ingredient.item != null)
            {
                ingredientName = ingredient.item.Name;
            }
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width - 20, EditorGUIUtility.singleLineHeight), ingredientName);
        };
        ingredientsList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Ingredients");
        ingredientsList.elementHeight = EditorGUIUtility.singleLineHeight * 2 + listVerticalItemSpacing;
        ingredientsList.elementHeightCallback = (index) =>
        {
            return EditorGUIUtility.singleLineHeight * 2.8f + listVerticalItemSpacing;
        };
        
    }
    public override void OnInspectorGUI()
    {
        recipeData = (Recipe)target;

        if (recipeData == null) return;

        ingredientsList.DoLayoutList();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add ingredient"))
        {
            recipeData.AddIngredient(new Ingredient(null, 0));
        }
        if (GUILayout.Button("Delete ingredient"))
        {
            recipeData.RemoveIngredient(recipeData.Ingredients.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        recipeData.CraftedItem = EditorGUILayout.ObjectField("Crafted item:",
            recipeData.CraftedItem, typeof(Item), true) as Item;

        GUILayout.Space(10);
        recipeData.CraftedNumber = EditorGUILayout.IntField("Crafted number:", recipeData.CraftedNumber);
        

        GUILayout.Space(10);
        if (recipeData.CraftedItem != null)
        {
            var craftedItemImageSize = Mathf.Min(EditorGUIUtility.currentViewWidth, 200);
            GUILayout.Box(recipeData.CraftedItem.Image.texture, 
                GUILayout.Width(craftedItemImageSize), GUILayout.Height(craftedItemImageSize));
        }
        if (GUI.changed)
        {
            // записываем изменения над testScriptable в Undo
            Undo.RecordObject(target, "Test Scriptable Editor Modify");
            // помечаем тот самый testScriptable как "грязный" и сохраняем.
            EditorUtility.SetDirty(target);
        }
    }
}
