using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUse : MonoBehaviour
{
    public Item item;

    public virtual void StopUsing()
    {
        Destroy(this);
    }
}
