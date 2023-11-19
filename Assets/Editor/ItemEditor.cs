using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override bool HasPreviewGUI() => true;

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        // Draw the custom preview in the Assets window
        Item itemData = (Item)target;
        
        if (itemData == null) return;

        GUI.DrawTexture(r, itemData.Image.texture);
    }
}
