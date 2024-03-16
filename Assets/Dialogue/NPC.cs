using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEditor;
using UnityEngine;

public class NPC : Interactable
{
    public override void EnterInteractionArea(GameObject interactor) {}
    public override void LeaveInteractionArea(GameObject interactor) {}

    public bool locked = false;
    public override void Interact(GameObject interactor)
    {
        var questPoint = GetComponent<QuestPoint>();
        if (questPoint)
        {
            if (!questPoint.startPoint)
            {
                if (FindObjectOfType<WaitForDialogueStep>())
                {
                    locked = false;
                }
                else
                {
                    locked = true;
                }
            }
            else
            {
                GameManager.instance.questActions.StartQuest(questPoint.questId);
            }
        }
    }
    
    
    
    
}