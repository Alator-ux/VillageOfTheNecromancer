using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    private float distanceToClosest;
    private Interactable closest;
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
        closest?.Interact(this.gameObject);
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
    }
}
