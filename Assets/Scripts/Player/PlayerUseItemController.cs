using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItemController : MonoBehaviour
{
    //public ItemUse ItemInUse { get; private set; } = null;
    public ItemUse ItemInUse;

    public void UseItem(Item item)
    {
        if (ItemInUse != null)
        {
            StopUsingItem();
        }
        if(item == null)
        {
            return;
        }

        ItemInUse = gameObject.GetComponent(item.UseScriptName) as ItemUse;

        if (ItemInUse == null)
            return;

        ItemInUse.item = item;
        ItemInUse.enabled = true;
    }

    public void StopUsingItem()
    {
        ItemInUse?.StopUsing();
        ItemInUse = null;
    }
}
