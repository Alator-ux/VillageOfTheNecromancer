using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : Interactable
{
    
    //[SerializeField] private GameObject dialogue;
    
    public override void EnterInteractionArea(GameObject interactor) {}
    public override void LeaveInteractionArea(GameObject interactor) {}
    public override void Interact(GameObject interactor)
    {
        Debug.Log("NPC Interacted!");
        
    }
    
    public KeyCode interactKey = KeyCode.E; 

    private bool playerInRange = false;
    
}
