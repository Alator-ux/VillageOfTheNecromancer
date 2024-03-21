using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawnScrollUse : ItemUse
{
    private SkeletonSpawnScroll scroll;

    [SerializeField]
    private Transform[] spawnPoints;

    private Inventory inventory;
    private EnergyController energyController;

    private void OnEnable()
    {
        scroll = item as SkeletonSpawnScroll;

        if (scroll == null)
        {
            throw new ApplicationException("item must be scroll");
        }

        if (spawnPoints.Length < scroll.skeletonsCount) {
            throw new ApplicationException("Spawn points count should not be less than skeletons count");
        }
    }

    void Start() {
        inventory = GetComponent<Inventory>();
        energyController = GetComponent<EnergyController>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && energyController.ConsumeEnergy()) {
            Debug.Log(energyController.EnergyPoints);
            SpawnSkeletons();
        }
    }

    public void SpawnSkeletons() {
        for (int i = 0; i < scroll.skeletonsCount; i++) {
            Vector3 spawnPoint = spawnPoints[i].position;
            var skeleton = Instantiate(scroll.skeletonPrefab, spawnPoint, Quaternion.identity);
            skeleton.resourceType = scroll.resourceType;
        }

        inventory.RemoveItem(item, 1);
        StopUsing();
    }
}