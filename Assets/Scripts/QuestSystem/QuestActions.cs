using UnityEngine;
using System;

public class QuestActions
{
    public Action onSeedCollected;

    public void SeedCollected()
    {
       
        if (onSeedCollected != null)
        {
            Debug.Log("bh");
            onSeedCollected();
        }
    }
    
}
