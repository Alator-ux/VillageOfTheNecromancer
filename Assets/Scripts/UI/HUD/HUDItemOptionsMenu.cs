using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum HUDItemOptionsMenuAction { Use, Drop, DropAll, Cancel};

public class HUDItemOptionsMenu : OptionsMenu
{
    public void SetOnButtonClickCallback(Action<HUDItemOptionsMenuAction> callback)
    {
        buttons[(int)HUDItemOptionsMenuAction.Use].onClick.AddListener(() => { callback(HUDItemOptionsMenuAction.Use); });

        buttons[(int)HUDItemOptionsMenuAction.Drop].onClick.AddListener(() => { callback(HUDItemOptionsMenuAction.Drop); });

        buttons[(int)HUDItemOptionsMenuAction.DropAll].onClick.AddListener(() => { callback(HUDItemOptionsMenuAction.DropAll); });

        buttons[(int)HUDItemOptionsMenuAction.Cancel].onClick.AddListener(() => { callback(HUDItemOptionsMenuAction.Cancel); });
    }
}
