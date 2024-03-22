using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
using UnityEngine.Android;

public class PlayerInteractionManager : MonoBehaviour
{
    private float distanceToClosest;
    [SerializeField] private Interactable closest;

    [Header("DialogueRelated")]

    private DialogueManager dialogueManager;

    [SerializeField] ArticyObject availableDialogue;


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public PlayerInteractionManager()
    {
        Reset();
    }
    public void ReplaceObject(Interactable newInteractable, Vector2 playerPos)
    {
        Vector2 gameObjectPosition = new Vector2(newInteractable.transform.position.x,
                                                    newInteractable.transform.position.y);
        float newDistance = Vector2.Distance(gameObjectPosition, playerPos);

        if (newInteractable.gameObject == closest?.gameObject)
        {
            distanceToClosest = newDistance;
            return;
        }

        if (newDistance > distanceToClosest)
        {
            return;
        }

        closest?.LeaveInteractionArea(this.gameObject);

        closest = newInteractable;
        distanceToClosest = newDistance;

        closest.EnterInteractionArea(this.gameObject);

    }
    public void StopInteraction(Interactable interactable)
    {
        if (interactable != this.closest)
        {
            return;
        }

        closest.LeaveInteractionArea(this.gameObject);
        Reset();
    }

    public void Interact()
    {
        if (closest != null)
        {
//            Debug.Log($"Closest: {closest}");
            closest.Interact(this.gameObject);
        }

        if (availableDialogue)
        {
            closest.Interact(this.gameObject);
            if(!closest.GetComponent<NPC>().locked)
                dialogueManager.StartDialogue(availableDialogue);
        }
    }

    private void Reset()
    {
        distanceToClosest = float.MaxValue;
        closest = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            var interactable = collision.GetComponent<Interactable>();
            if (!interactable)
            {
                interactable = collision.GetComponentInParent<Interactable>();
            }

            var playerPosition = new Vector2(transform.position.x, transform.position.y);
            ReplaceObject(interactable, playerPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            var interactable = collision.GetComponent<Interactable>();
            StopInteraction(interactable);
        }

        if (collision.CompareTag("NPC"))
        {
            if (collision.GetComponent<ArticyReference>() != null)
            {
                availableDialogue = null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            var articyReferenceComp = collision.GetComponent<ArticyReference>();
            if (articyReferenceComp)
            {
                availableDialogue = articyReferenceComp.reference.GetObject();
            }

           /* var q = collision.gameObject.GetComponent<QuestPoint>();
            if (q)
            {
                GameManager.instance.questActions.AdvanceQuest(q.questId);
            }*/

        }
    }

}
