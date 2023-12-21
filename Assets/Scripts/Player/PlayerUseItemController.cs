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
        var itemUseType = item.ItemUseScriptType;
        if (itemUseType == null)
            return;

        ItemInUse = (ItemUse) gameObject.AddComponent(itemUseType);
        ItemInUse.item = item;
    }

    public void StopUsingItem()
    {
        ItemInUse?.StopUsing();
        ItemInUse = null;
    }
}
