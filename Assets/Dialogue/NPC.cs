using System.Collections;
using System.Collections.Generic;
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
            GameManager.instance.questActions.AdvanceQuest(questPoint.questId);
        }
    }
    
    public KeyCode interactKey = KeyCode.E; 

    private bool playerInRange = false;
    
}