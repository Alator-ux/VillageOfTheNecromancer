using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHoeStep : QuestStep
{

    private void OnEnable()
    {
        Transform hoe = GameObject.Find("Hoe").GetComponent<Transform>();
        foreach (Transform t in hoe)
        {
            t.gameObject.SetActive(true);
        }
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