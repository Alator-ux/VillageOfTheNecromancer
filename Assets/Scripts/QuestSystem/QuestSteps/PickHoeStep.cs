using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHoeStep : QuestStep
{

    private void OnEnable()
    {
        GameObject.Find("Hoe").GetComponent<PickableItem>().enabled = true;
        GameManager.instance.questActions.onHoePicked += PickHoe;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onHoePicked -= PickHoe;
    }

    public void PickHoe()
    {
        FinishQuestStep();
    }


}