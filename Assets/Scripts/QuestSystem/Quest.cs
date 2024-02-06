using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoStatic info;

    public QuestState state;

    private int currentQuestStepIndex;

    public Quest(QuestInfoStatic questInfoStatic)
    {
        this.info = questInfoStatic;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
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
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
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