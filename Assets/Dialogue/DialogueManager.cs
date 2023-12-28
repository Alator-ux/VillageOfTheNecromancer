using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Reference to your text components in Unity
    public Text characterNameText;
    public Text dialogueText;

    // Path to your .articyu3d file
    public string articyu3dFilePath;

    // List to store parsed dialogue data
    private List<DialogueObject> dialogues;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = ParseArticyu3dFile(articyu3dFilePath);


        StartDialogue(0); 
    }
    
    private List<DialogueObject> ParseArticyu3dFile(string filePath)
    {

        List<DialogueObject> parsedDialogues = new List<DialogueObject>();

        return parsedDialogues;
    }

    // Function to start a dialogue by index
    private void StartDialogue(int index)
    {
        if (index >= 0 && index < dialogues.Count)
        {
            DialogueObject currentDialogue = dialogues[index];
            
            characterNameText.text = currentDialogue.characterName;
            dialogueText.text = currentDialogue.dialogueLine;
        }
    }
}
