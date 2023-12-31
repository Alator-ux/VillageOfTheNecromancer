using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask blockingLayer;

    protected BoxCollider2D boxCollider;

    private Rigidbody2D rb2D;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected void Move(Vector2 moveDirection)
    {
        moveDirection.Normalize();
        Vector2 start = rb2D.position;
        Vector2 end = start + moveDirection * speed * Time.fixedDeltaTime;
        rb2D.MovePosition(end);
    }
}
