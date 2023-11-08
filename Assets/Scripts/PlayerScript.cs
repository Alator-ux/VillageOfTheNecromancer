using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    class PlayerInteractionManager
    {
        private float distance;
        private Interactable interactableObject;
        private GameObject gameObject;
        public PlayerInteractionManager()
        {
            Reset();
        }
        public void ReplaceObject(GameObject gameObject, Vector2 playerPos)
        {
            Vector2 gameObjectPosition = new Vector2(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y);
            float newDistance = Vector2.Distance(gameObjectPosition, playerPos);

            if(gameObject == this.gameObject)
            {
                distance = newDistance;
                return;
            }

            if (newDistance > distance)
            {
                return;
            }

            interactableObject?.HideMessage();

            this.gameObject = gameObject;
            interactableObject = gameObject.GetComponent<Interactable>(); ;

            interactableObject.ShowMessage();

        }
        public void ExitCollider(GameObject gameObject)
        {
            if(gameObject != this.gameObject)
            {
                return;
            }

            interactableObject.HideMessage();
            Reset();
        }
        public void Interact()
        {
            interactableObject?.Interact();
        }
        private void Reset()
        {
            distance = float.MaxValue;
            gameObject = null;
            interactableObject = null;
        }
    }
    public float playerSpeed = 1f;
    private PlayerInteractionManager interactionManager;
    protected override void Start()
    {
        interactionManager = new PlayerInteractionManager();
        base.Start();
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(0f, 0f);
        moveDirection.x = (float)Input.GetAxisRaw("Horizontal");
        moveDirection.y = (float)Input.GetAxisRaw("Vertical");
        Move(moveDirection);

        if (Input.GetButtonDown("Interact"))
        {
            interactionManager.Interact();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactionManager.ReplaceObject(collision.gameObject,
                new Vector2(transform.position.x, transform.position.y));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactionManager.ExitCollider(collision.gameObject);
        }
    }
}
