using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    private BoxCollider2D triggerCollider;
    
    float colliderSizeMultiplier;

    public Interactable(float colliderSizeMultiplier = 1f)
    {
        this.colliderSizeMultiplier = colliderSizeMultiplier;
    }

    protected void Start()
    {
        tag = "Interactable";
        triggerCollider = GetComponent<BoxCollider2D>();
        if (triggerCollider == null)
        {
            CreateBoxCollider();
        }
    }

    private void CreateBoxCollider()
    {
        if (triggerCollider != null)
        {
            Destroy(triggerCollider);
        }

        triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.size = new Vector2(1f * colliderSizeMultiplier, 1f * colliderSizeMultiplier);
        triggerCollider.isTrigger = true;
    }

    public abstract void Interact(GameObject interactor);
    public abstract void EnterInteractionArea(GameObject interactor);
    public abstract void LeaveInteractionArea(GameObject interactor);
}
