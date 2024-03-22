using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    private bool playerIsNear = false;

    [Header("Quest")]
    [SerializeField] private QuestInfoStatic questInfoForPoint;

    public string questId;

    [Header("Config")]
    [SerializeField] public bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameManager.instance.questActions.onQuestStateChange += QuestStateChange;
        GameManager.instance.questActions.onQuestAdvance += QuestAdvance;
        GameManager.instance.questActions.onQuestStart += QuestStart;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onQuestStateChange -= QuestStateChange;
        GameManager.instance.questActions.onQuestAdvance -= QuestAdvance;
        GameManager.instance.questActions.onQuestStart -= QuestStart;
    }

    public void DialogueFinished()
    {

    }
    public void QuestStart(string questid)
    {
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            QuestManager.instance.StartQuest(questid);
        }

    }

    public void QuestAdvance(string questid)
    {
        startPoint = false;
        if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameManager.instance.questActions.FinishQuest(questId);
        }
    }

    public void QuestStateChange(Quest quest)
    {

        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }

}
