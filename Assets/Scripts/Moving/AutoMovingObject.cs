using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoMovingObject : MovingObject
{
    [SerializeField]
    float checkPointInterval = 50;
    bool isDestinationReached = true;
    float checkPointTime;
    bool isMoving = false;
    PathFinder pathFinder;
    List<Vector2> path = new List<Vector2>();
    int currentWayPoint = 0;
    Coroutine followingCoroutine;
    Vector2 destination;
    public bool IsMoving { get => isMoving; }

    override protected void Start()
    {
        base.Start();
        var radius = (float)Math.Sqrt(Math.Pow(boxCollider.size.x, 2) + Math.Pow(boxCollider.size.y, 2));
        pathFinder = new PathFinder(radius);

        MoveToPosition(new Vector2(4.4f, -2.85f));

    }
    private void Update()
    {
        if(!IsMoving && !isDestinationReached && checkPointTime > Time.fixedTime)
        {
            MoveToPosition(destination);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving)
        {
            var destination = path[path.Count - 1];
            isMoving = false;
            currentWayPoint = -1;
            path = null;
            isDestinationReached = true;
            StopCoroutine(followingCoroutine);
            MoveToPosition(destination);
        }
        
    }
    public void MoveToPosition(Vector2 worldPosition)
    {
        if (isMoving)
        {
            return;
        }

        destination = worldPosition;
        checkPointTime = Time.fixedTime + checkPointInterval;

        boxCollider.enabled = false;
        path = pathFinder.FindPath(transform.position.ToVector2(), worldPosition);
        boxCollider.enabled = true;

        isDestinationReached  = false;
        if (path == null)
        {
            return;
        }

        path.Add(worldPosition);
        currentWayPoint = 1;
        followingCoroutine = StartCoroutine(FollowThePath());
    }

    IEnumerator FollowThePath()
    {
        isMoving = true;
        while (currentWayPoint < path.Count)
        {
            Vector3 targetPosition = path[currentWayPoint].ToVector3(transform.position.z);
            while (!transform.position.NearlyEqual(targetPosition))
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                yield return null;
            }
            currentWayPoint++;
        }

        isDestinationReached = true;
        path = null;
        currentWayPoint = -1;
        isMoving = false;
    }
}
