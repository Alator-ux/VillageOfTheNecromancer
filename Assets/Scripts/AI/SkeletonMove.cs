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
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
