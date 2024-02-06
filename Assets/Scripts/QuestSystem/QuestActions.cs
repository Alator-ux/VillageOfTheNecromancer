using UnityEngine;
using System;

public class QuestActions 
{
    public event Action onSeedCollected;

    public void SeedCollected()
    {
        Debug.Log("bh");
        if (onSeedCollected != null)
        {
            onSeedCollected();
        }
    }
    
}
