using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask blockingLayer;

    protected BoxCollider2D boxCollider;

    protected Rigidbody2D rb2D;
    public bool FacingRight {get; private set; } = true;
    public bool FacingLeft => !FacingRight;

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

        if (moveDirection.x > 0 && !FacingRight)
            Flip();
        if (moveDirection.x < 0 && FacingRight)
            Flip();
    }

    public void Flip() {
        FacingRight = !FacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
