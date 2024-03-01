using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    private bool playerIsNear = false;
    
    [SerializeField] private QuestInfoStatic questInfo;
    public string questId;
    private QuestState currentQuestState;
    
    private void Awake() 
    {
        questId = questInfo.id;
    }

    private void OnEnable()
    {
        GameManager.instance.questActions.onQuestStateChange += QuestStateChange;
        GameManager.instance.questActions.onQuestAdvance += QuestAdvance;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onQuestStateChange -= QuestStateChange;
    }

    public void QuestAdvance(string questid)
    {
        if (currentQuestState == QuestState.REQUIREMENTS_NOT_MET)
        {
            currentQuestState = QuestState.IN_PROGRESS;
        }
        Debug.Log("Quest advanced");
    }

    public void QuestStateChange(Quest quest)
    {
        
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }

}
