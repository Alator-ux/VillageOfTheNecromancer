
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimatedPlant : Plant
{
    protected Animator animator;
    private GameObject lastInteractor;

    protected new void Start() {
        animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void UpdateRendering()
    {
        switch(CurrentState) {
            case PlantState.Seed:
                animator.SetBool("IsChild", false);
                animator.SetBool("IsGrown", false);
                break;
            case PlantState.Child:
                animator.SetBool("IsChild", true);
                animator.SetBool("IsGrown", false);
                break;
            case PlantState.Grown:
                animator.SetBool("IsChild", false);
                animator.SetBool("IsGrown", true);
                break;
        }
    }

    public override void Interact(GameObject interactor) {
        if (interactor == null || CurrentState != PlantState.Grown) return;

        if (CanPickUp(interactor)) {
            lastInteractor = interactor;
            animator.SetBool("Collect", true);
        }
    }

    protected void OnCollectAnimationEnded() {
        animator.SetBool("Collect", false);
        OnPickUp(lastInteractor);

        AfterPickUp();
    }
}
