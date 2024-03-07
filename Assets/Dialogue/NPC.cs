using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEditor;
using UnityEngine;

public class NPC : Interactable
{
    public override void EnterInteractionArea(GameObject interactor) {}
    public override void LeaveInteractionArea(GameObject interactor) {}
    public override void Interact(GameObject interactor)
    {
        var questPoint = GetComponent<QuestPoint>();
        if (questPoint)
        {
            if (!questPoint.startPoint)
            {
                //GameManager.instance.questActions.AdvanceQuest(questPoint.questId);
            }
            else
            {
                GameManager.instance.questActions.StartQuest(questPoint.questId);
            }
        }
    }

    void UpdateDialogue()
    {
        var referen = GetComponent<ArticyRef>();

    }
    
    public KeyCode interactKey = KeyCode.E; 

    private bool playerInRange = false;
    
}