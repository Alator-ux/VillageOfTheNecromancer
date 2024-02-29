using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;


public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [Header("UI")]
    // Reference to Dialog UI
    [SerializeField]
    GameObject dialogueWidget;
    // Reference to dialogue text
    [SerializeField]
    TextMeshProUGUI dialogueText;
    // Reference to speaker
    [SerializeField]
    TextMeshProUGUI dialogueSpeaker;

    // To check if we are currently showing the dialog ui interface
    public bool DialogueActive { get; set; }

    private ArticyFlowPlayer flowPlayer;

    void Start()
    {
        flowPlayer = GetComponent<ArticyFlowPlayer>();
    }
    
    public void StartDialogue(IArticyObject aObject)
    {
        DialogueActive = true;
        dialogueWidget.SetActive(DialogueActive);
        flowPlayer.StartOn = aObject;
    }

    public void CloseDialogueBox()
    {
        DialogueActive = false;
        dialogueWidget.SetActive(DialogueActive);        
    }

    // This is called every time the flow player reaches an object of interest
    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        //Clear data
        dialogueText.text = string.Empty;

        Debug.Log(aObject.GetType());
        // If we paused on an object that has a "Text" property fetch this text and present it        
        var objectWithText = aObject as IObjectWithLocalizableText;
        if (objectWithText != null)
        {
            dialogueText.text = objectWithText.Text;
        }
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        
    }
}