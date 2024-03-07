using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            
            //GameManager.instance.questActions.AdvanceQuest(questId);
             
            Destroy(this.gameObject);
        }
    }
}
