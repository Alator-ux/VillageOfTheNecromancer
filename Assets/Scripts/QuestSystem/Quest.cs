using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoStatic info;

    public QuestState state;

    public int currentQuestStepIndex;

    public Quest(QuestInfoStatic questInfoStatic)
    {
        this.info = questInfoStatic;
        this.state = QuestState.CAN_START;
        this.currentQuestStepIndex = 0;
    }

    public void MoveNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepsPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if (questStepPrefab != null)
        {
            var newStep = Object.Instantiate(questStepPrefab, parentTransform);
        }
        
    }

    public GameObject GetCurrentQuestStepPrefab()
    {
        GameObject currentStepPrefab = null;
        
        if (CurrentStepExists())
        {
            currentStepPrefab = info.questStepsPrefabs[currentQuestStepIndex];
        }
        
        return currentStepPrefab;
    }
}
