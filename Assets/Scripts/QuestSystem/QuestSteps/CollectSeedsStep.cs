using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSeedsStep : QuestStep
{
    private int seedsCollected = 0;

    private int seedsToCollect = 3;
    
    [SerializeField] private GameObject seedPrefab;

    private void OnEnable()
    {
        var location = GameObject.Find("Items").transform.Find("Seeds");
        for (int i = 0; i < 3; i++)
        {
            Instantiate(seedPrefab, new Vector3(location.position.x + i, location.position.y, location.position.z), Quaternion.identity);
        }
        
        GameManager.instance.questActions.onSeedCollected += SeedCollected;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onSeedCollected -= SeedCollected;
    }

    public void SeedCollected()
    {
        if (seedsCollected < seedsToCollect)
        {
            seedsCollected++; 
        }   

        if (seedsCollected >= seedsToCollect)
        {
            FinishQuestStep();
        }
    }


}
