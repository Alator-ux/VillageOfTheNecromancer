using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCollector : MonoBehaviour
{
    public enum State {
        Idle,
        WalkToResource,
        Collect,
    }

    [SerializeField]
    private int lifes = 10;

    public Item resourceType;
    public State CurrentState { get; private set; }
    private Resource resource;
    private ResourceManager resourceManager;
    private Animator animator;
    private SkeletonMove skeletonMove;

    private void Start() {
        CurrentState = State.Idle;
        resourceManager = FindObjectOfType<ResourceManager>();
        animator = GetComponent<Animator>();
        skeletonMove = GetComponent<SkeletonMove>();
        skeletonMove.OnTargetReached += ResourceReached;
    }

    private void ResourceReached() {
        CurrentState = State.Collect;
    }

    private void Update() {
        if (lifes == 0) {
            Die();
            return;
        }

        switch (CurrentState) {
            case State.Idle:
                animator.SetBool("Walking", false);
                animator.SetBool("Collecting", false);
                resource = resourceManager.GetResourceForActor(this);

                if (resource == null) return;

                CurrentState = State.WalkToResource;
                skeletonMove.SetTarget(resource.transform);
                break;
            case State.WalkToResource:
                animator.SetBool("Walking", true);
                break;
            case State.Collect:
                animator.SetBool("Walking", false);
                animator.SetBool("Collecting", true);
                break;
        }
    }

    private void Die() {
        resourceManager.FreeResource(this);
        Destroy(gameObject);
    }

    public void OnCollectAnimationEnded() {
        if (resource == null) {
            CurrentState = State.Idle;
            return;
        }

        Item retrievableItem = resource.RetrievableItem;
        resource.Retrieve(1);

        float offsetX = Random.Range(-2.0f, 2.0f);
        float offsetY = Random.Range(-2.0f, 2.0f);
        Vector3 offset = new Vector3(offsetX, offsetY, 0f);
        retrievableItem.Drop(resource.transform.position + offset);

        lifes--;
    }
}
