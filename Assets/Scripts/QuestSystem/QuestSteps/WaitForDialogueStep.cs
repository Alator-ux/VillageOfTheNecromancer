using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDialogueStep : QuestStep
{

    private void OnEnable()
    {
        GameManager.instance.questActions.onDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        GameManager.instance.questActions.onDialogueFinished -= DialogueFinished;
    }

    void DialogueFinished()
    {
        FinishQuestStep();
    }
}
