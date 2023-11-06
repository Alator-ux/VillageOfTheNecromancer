using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string message;
    protected GameObject messageObject;
    public BoxCollider2D collder;
    InteractableObject(bool createBoxColider2D, bool createMessageObject, string message = "")
    {
        this.message = message;
        //messageObject = 
        //messageObject.HideMessage();
    }
    public virtual bool CanInteract()
    {
        return true;
    }
    public virtual void Interact() {
        Debug.Log("Interacted");
    }
    public void ShowMessage()
    {
        messageObject?.SetActive(true);
    }
    public void HideMessage()
    {
        messageObject?.SetActive(false);
    }
}
