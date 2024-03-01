using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private bool startQuestPoint;
    [SerializeField] private bool finishQuestPoint;
    public override void EnterInteractionArea(GameObject interactor) {}
    public override void LeaveInteractionArea(GameObject interactor) {}
    public override void Interact(GameObject interactor)
    {
        
        
    }
    
    public KeyCode interactKey = KeyCode.E; 

    private bool playerInRange = false;
    
}