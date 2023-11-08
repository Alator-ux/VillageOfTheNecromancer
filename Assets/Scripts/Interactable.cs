using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class Interactable: MonoBehaviour
{
    public GameObject messageObject;
    public BoxCollider2D triggerCollider;
    protected bool canInteract = true;
    public bool CanInteract {
        get
        {
            return canInteract;
        }
    }

    bool createBoxColider2D, createMessageObject;
    float bcSizeMult;
    string message;
    public Interactable(bool createBoxColider2D = false, float bcSizeMult = 0f, 
        bool createMessageObject = false, string message = "")
    {
        this.createBoxColider2D = createBoxColider2D;
        this.createMessageObject = createMessageObject;
        this.bcSizeMult = bcSizeMult;
        this.message = message;
    }
    private void Start()
    {
        tag = "Interactable";
        if (createBoxColider2D)
        {
            if(triggerCollider != null)
            {
                Destroy(triggerCollider);
            }

            triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            triggerCollider.size = new Vector2(1f * bcSizeMult, 1f * bcSizeMult);
            triggerCollider.isTrigger = true;
        }
        if (createMessageObject)
        {
            if(messageObject != null)
            {
                Destroy(messageObject);
            }

            var canvasObject = new GameObject("MessageCanvas");
            canvasObject.transform.SetParent(gameObject.transform);
            messageObject = canvasObject;

            var messageCanvas = canvasObject.AddComponent<Canvas>();
            messageCanvas.renderMode = RenderMode.WorldSpace;
            messageCanvas.sortingLayerName = "UI";

            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            
            var messageCanvasRT = canvasObject.GetComponent<RectTransform>();
            messageCanvasRT.transform.localPosition = new Vector3(1, 1, 0);
            messageCanvasRT.sizeDelta = new Vector2(2f, 2f);
            
            var textObject = new GameObject("MessageText");
            textObject.transform.SetParent(canvasObject.transform);

            var textComponent = textObject.gameObject.AddComponent<TextMeshProUGUI>();
            textComponent.text = message;
            textComponent.fontSize = 0.3f;
            textComponent.alignment = TextAlignmentOptions.BottomLeft;

            var textRT = textComponent.GetComponent<RectTransform>();
            textRT.anchorMin = new Vector2(0f, 0f);
            textRT.anchorMax = new Vector2(1f, 1f);
            textRT.pivot = new Vector2(1f, 1f);
            textRT.offsetMin = new Vector2(0f, 0f);
            textRT.offsetMax = new Vector2(0f, 0f);
        }
        HideMessage();
    }
    public virtual void Interact() {
        if (canInteract)
        {
            Debug.Log("Interacted");
        }
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
