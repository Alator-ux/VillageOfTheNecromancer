using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCropStep : QuestStep
{
    
    [SerializeField] int cropCollected = 0;

    [SerializeField] int cropToCollect = 3;

    private void OnEnable()
    {
        GameManager.instance.questActions.onCropCollected += CropCollected;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onCropCollected -= CropCollected;
    }

    public void CropCollected()
    {
        if (cropCollected < cropToCollect)
        {
            cropCollected++; 
        }   

        if (cropCollected >= cropToCollect)
        {
            FinishQuestStep();
        }
    }


}