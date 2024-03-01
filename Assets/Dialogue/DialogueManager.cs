using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.UnityImporterTutorial;
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

    [SerializeField] private Button ProceedButton;

    // To check if we are currently showing the dialog ui interface
    public bool DialogueActive { get; set; }

    private ArticyFlowPlayer flowPlayer;

    void Start()
    {
        flowPlayer = GetComponent<ArticyFlowPlayer>();
        ProceedButton.onClick.AddListener(ContinueDialogue);
    }

    private void ContinueDialogue()
    {
        flowPlayer.Play();
    }
    
    public void StartDialogue(IArticyObject aObject)
    {
        if (aObject != null)
        {
            DialogueActive = true;
            dialogueWidget.SetActive(DialogueActive);
            flowPlayer.StartOn = aObject;
        }
    }

    public void CloseDialogueBox()
    {
        DialogueActive = false;
        dialogueWidget.SetActive(DialogueActive);        
    }

    // This is called every time the flow player reaches an object of interest
    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        dialogueText.text = string.Empty;
        dialogueSpeaker.text = string.Empty;

        Debug.Log(aObject.GetType());
        var objectWithText = aObject as IObjectWithLocalizableText;
        if (objectWithText != null)
        {
            dialogueText.text = objectWithText.Text;
        }

        var objectWithSpeaker = aObject as IObjectWithSpeaker;
        if (objectWithSpeaker != null)
        {
            var speakerEntity = objectWithSpeaker.Speaker as Entity;
            if (speakerEntity != null)
            {
                dialogueSpeaker.text = speakerEntity.DisplayName;
            }
        }
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        bool dialogueIsFinished = true;
        foreach (var branch in aBranches)
        {
            if (branch.Target is IDialogueFragment)
            {
                dialogueIsFinished = false;
            }

            if (dialogueIsFinished)
            {
                dialogueWidget.SetActive(false);
            }
        }
    }
}