using UnityEngine;
using System;

public class QuestActions
{
    public Action<string> onQuestStart;

    public void StartQuest(string id)
    {
        if (onQuestStart != null)
        {
            onQuestStart(id);
        }
    }
    
    public Action<string> onQuestFinish;

    public void FinishQuest(string id)
    {
        if (onQuestFinish != null)
        {
            onQuestFinish(id);
        }
    }
    
    public Action<string> onQuestAdvance;

    public void AdvanceQuest(string id)
    {
        if (onQuestAdvance != null)
        {
            onQuestAdvance(id);
        }
    }
    
    public Action<Quest> onQuestStateChange;

    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }
    
    
    public Action onSeedCollected;

    public void SeedCollected()
    {
       
        if (onSeedCollected != null)
        {
            onSeedCollected();
        }
    }
    
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null) 
        {
            onSubmitPressed();
        }
    }
    
}
