using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private int stepIndex;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            
            Destroy(this.gameObject);
            
            GameManager.instance.questActions.AdvanceQuest(QuestManager.instance.currentQuest.info.id);
             
            
        }
    }
}
