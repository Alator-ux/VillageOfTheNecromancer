using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMove : MonoBehaviour
{
    public bool Moving { get; private set; }
    public Action OnTargetReached;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float distanceToStop = 1.0f;
    private Transform target = null;
    private bool facingRight = false;

    public void SetTarget(Transform newTarget) {
        target = newTarget;
        Moving = true;
    }

    public void UnsetTarget() {
        target = null;
        Moving = false;
    }


    private void Update() {
        if (target == null || !Moving) {
            return;
        }

        var distance = (target.position - transform.position).magnitude;
        if (distance < distanceToStop) {
            OnTargetReached?.Invoke();
            Moving = false;
        }

        var moveDirection = target.position - transform.position;
        if (moveDirection.x > 0 && !facingRight)
            Flip();
        if (moveDirection.x < 0 && facingRight)
            Flip();

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void Flip() {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
