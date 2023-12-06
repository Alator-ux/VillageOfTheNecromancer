using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject optionsMenuPrefab;
    private GameObject optionsMenu;
    private CanvasGroup topLevelObjectCG;

    private GameObject topLevelObject;
    public GameObject TopLevelObject
    {
        set
        {
            topLevelObject = value;
            topLevelObjectCG = topLevelObject.GetComponent<CanvasGroup>();
        }
    }

    public OptionsMenu CreateOptionsMenu(Vector2 position)
    {
        DestroyOptionsMenu();
        optionsMenu = Instantiate(optionsMenuPrefab, topLevelObject.transform);
        optionsMenu.transform.position = position + new Vector2(60, 0);
        topLevelObjectCG.blocksRaycasts = true;
        var optionsMenuScript = optionsMenu.GetComponent<OptionsMenu>();
        optionsMenuScript.SetOnButtonClickCallback(() => { topLevelObjectCG.blocksRaycasts = false; });
        return optionsMenuScript;
    }
    public void DestroyOptionsMenu()
    {
        Destroy(optionsMenu);
    }
}
