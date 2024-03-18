using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableObject : MonoBehaviour
{
    private string id;
    public string Id
    {
        get => id;
        set => id = value;
    }

    private string uniqueId;
    public string UniqueId
    {
        get => uniqueId;
    }

    public void Start()
    {
        Guid uniqueId = Guid.NewGuid();
        this.uniqueId = uniqueId.ToString();

        id = string.Empty;
    }

    public bool WeakEqual(SelectableObject other)
    {
        return id == other.id;
    }
    public bool StrongEqual(SelectableObject other)
    {
        return uniqueId == other.uniqueId;
    }
    public bool FullEqual(SelectableObject other)
    {
        return WeakEqual(other) && StrongEqual(other);
    }
}
