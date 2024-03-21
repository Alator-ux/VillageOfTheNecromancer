using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHoeStep : QuestStep
{
    [SerializeField] private GameObject hoePrefab;
    private void OnEnable()
    {
        var location = GameObject.Find("Items").transform.Find("Hoe");
        Instantiate(hoePrefab, location);
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