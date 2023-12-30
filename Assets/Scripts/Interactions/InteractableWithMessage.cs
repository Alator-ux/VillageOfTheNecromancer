using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class InteractableWithMessage : Interactable
{
    public InteractableWithMessage(string message = "", float colliderSizeMultiplier = 0f)
            : base(colliderSizeMultiplier)
    {
        this.message = message;
    }

    [SerializeField]
    private GameObject messageObject = null;

    private TextMeshProUGUI textComponent;
    private string message;

    public string Message
    {
        get { return message; }
        set { 
            message = value;
            textComponent.text = message;
        }
    }

    protected new void Start()
    {
        base.Start();
        messageObject = GetComponent<GameObject>();
        if (messageObject == null)
        {
            CreateMessageObject();
        }
        messageObject?.SetActive(false);
    }

    private void CreateMessageObject()
    {
        if (messageObject != null)
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

        textComponent = textObject.gameObject.AddComponent<TextMeshProUGUI>();
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

    public override void EnterInteractionArea(GameObject interactor)
    {
        messageObject?.SetActive(true);
    }

    public override void LeaveInteractionArea(GameObject interactor)
    {
        messageObject?.SetActive(false);
    }
}
