using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private GameObject dialogue;
    
    public override void EnterInteractionArea(GameObject interactor) {}
    public override void LeaveInteractionArea(GameObject interactor) {}
    public override void Interact(GameObject interactor)
    {
        if (dialogue)
        {
            dialogue.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
