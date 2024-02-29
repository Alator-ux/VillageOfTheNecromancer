using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    private bool playerIsNear = false;
    
    [SerializeField] private QuestInfoStatic questInfo;
    private string questId;
    private QuestState currentQuestState;
    
    private void Awake() 
    {
        questId = questInfo.id;
    }

    private void OnEnable()
    {
        GameManager.instance.questActions.onQuestStateChange += QuestStateChange;
        GameManager.instance.questActions.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onQuestStateChange -= QuestStateChange;
        GameManager.instance.questActions.onSubmitPressed -= SubmitPressed;
    }
    
    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }
        GameManager.instance.questActions.StartQuest(questId); 
        GameManager.instance.questActions.FinishQuest(questId);
    }

    public void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            
        }
    }
}
