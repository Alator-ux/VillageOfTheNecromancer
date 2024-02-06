using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public QuestActions questActions;
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            
        }

        instance = this;

        questActions = new QuestActions();
        Debug.Log(questActions);
    }
}
