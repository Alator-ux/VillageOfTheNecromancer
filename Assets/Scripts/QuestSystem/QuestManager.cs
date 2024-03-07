using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   private Dictionary<string, Quest> questMap;

   public Quest currentQuest;

   private QuestLog questLog;
   
   public static QuestManager instance { get; private set; }
   private void Awake()
   {
      if (instance != null)
      {
            
      }

      instance = this;
      
      
      
      questMap = CreateQuestMap();
      
   }

   private void OnEnable()
   {
      GameManager.instance.questActions.onQuestFinish += FinishQuest;
      GameManager.instance.questActions.onQuestAdvance += AdvanceQuest;
   }
   private void OnDisable()
   {
      GameManager.instance.questActions.onQuestFinish -= FinishQuest;
      GameManager.instance.questActions.onQuestAdvance -= AdvanceQuest;
   }

   void Start()
   {
      foreach (Quest quest in questMap.Values)
      {
         GameManager.instance.questActions.QuestStateChange(quest);
      }

      questLog = FindObjectOfType<QuestLog>(true);
      Debug.Log(questLog);
   }

   public void StartQuest(string id)
   {
      Quest quest = GetQuestById(id);
      currentQuest = quest;
      quest.InstantiateCurrentQuestStep(this.transform);
      ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
   }
   private void AdvanceQuest(string id)
   {
      Quest quest = GetQuestById(id);
      currentQuest = quest;

      quest.MoveNextStep();
      
      if (quest.CurrentStepExists())
      {
         quest.InstantiateCurrentQuestStep(this.transform);
      }
      else
      {
         ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
      }

      Debug.Log(quest.currentQuestStepIndex);
      if (quest.currentQuestStepIndex > 0)
      {
         questLog.DisplayInLog(quest.info.questLogMessages[quest.currentQuestStepIndex-1]);
      }
   }
   
   private void FinishQuest(string id)
   {
      Debug.Log("Finish quest" + id);
   }

   private Dictionary<string, Quest> CreateQuestMap()
   {
      QuestInfoStatic[] allQuests = Resources.LoadAll<QuestInfoStatic>("Quests");
      Dictionary<string, Quest> idToQuest = new Dictionary<string, Quest>();
      foreach (QuestInfoStatic quest in allQuests)
      {
         idToQuest.Add(quest.id, new Quest(quest));
      }

      return idToQuest;
   }

   private Quest GetQuestById(string id)
   {
      Quest quest = questMap[id];
      if (quest == null)
      {
         Debug.LogError("404 not found");
      }

      return quest;
   }

   private void ChangeQuestState(string id, QuestState state)
   {
      Quest quest = GetQuestById(id);
      quest.state = state;
      GameManager.instance.questActions.onQuestStateChange(quest);
   }
}
