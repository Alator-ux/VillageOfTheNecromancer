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
    
    public KeyCode interactKey = KeyCode.E; 

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void Interact()
    {
        Debug.Log("NPC Interacted!");
    }
}
