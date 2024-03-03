using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    Item retrievableItem;
    [SerializeField]
    int retrievableCapacity;

    public Item RetrievableItem { get => retrievableItem; }
    public int RetrievableCapacity { get => retrievableCapacity; }

    public int Retrieve(int count) {
        int retrieved = Math.Min(retrievableCapacity, count);
        retrievableCapacity -= retrieved;

        if (retrievableCapacity == 0)
            OnResourceEnded();

        return retrieved;
    }

    private void OnResourceEnded() {
        Destroy(gameObject);
    }
}
