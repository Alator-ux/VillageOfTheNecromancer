using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSeedsStep : QuestStep
{
    private int seedsCollected = 0;

    private int seedsToCollect = 3;

    private void OnEnable()
    {
        Transform seeds = GameObject.Find("Seeds").GetComponent<Transform>();
        foreach (Transform t in seeds)
        {
            t.gameObject.SetActive(true);
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
