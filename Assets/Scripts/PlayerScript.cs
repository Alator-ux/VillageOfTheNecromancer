using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    class PlayerInteractionManager
    {
        private float distance;
        private InteractableObject interactableObject;
        private GameObject gameObject;
        private float interactionRadius = 1f;
        public PlayerInteractionManager()
        {
            Reset();
        }
        public void ReplaceObject(GameObject gameObject, Vector2 playerPos)
        {
            Vector2 gameObjectPosition = new Vector2(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y);
            float newDistance = Vector2.Distance(gameObjectPosition, playerPos);
            if (newDistance > interactionRadius || newDistance > distance)
            {
                return;
            }

            interactableObject?.HideMessage();

            distance = newDistance;
            this.gameObject = gameObject;
            interactableObject = gameObject.GetComponent<InteractableObject>(); ;

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
        int xDir = (int)Input.GetAxisRaw("Horizontal");
        int yDir = (int)Input.GetAxisRaw("Vertical");

        //base.Move(xDir, yDir);
        transform.Translate(new Vector2(xDir, yDir) * playerSpeed * Time.fixedDeltaTime);

        if (Input.GetButton("Fire1"))
        {
            interactionManager.Interact();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            interactionManager.ReplaceObject(collision.gameObject,
                new Vector2(transform.position.x, transform.position.y));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            interactionManager.ExitCollider(collision.gameObject);
        }
    }
}
