using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   private Dictionary<string, Quest> questMap;
   private void Awake()
   {
      questMap = CreateQuestMap();
      
   }

   private void OnEnable()
   {
      GameManager.instance.questActions.onQuestStart += StartQuest;
      GameManager.instance.questActions.onQuestFinish += FinishQuest;
      GameManager.instance.questActions.onQuestAdvance += AdvanceQuest;
   }
   private void OnDisable()
   {
      GameManager.instance.questActions.onQuestStart -= StartQuest;
      GameManager.instance.questActions.onQuestFinish -= FinishQuest;
      GameManager.instance.questActions.onQuestAdvance -= AdvanceQuest;
   }

   void Start()
   {
      foreach (Quest quest in questMap.Values)
      {
         GameManager.instance.questActions.QuestStateChange(quest);
      }
   }

   private void StartQuest(string id)
   {
      Quest quest = GetQuestById(id);
      quest.InstantiateCurrentQuestStep(this.transform);
      ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
   }
   private void AdvanceQuest(string id)
   {
      Quest quest = GetQuestById(id);
      
      quest.MoveNextStep();
      
      if (quest.CurrentStepExists())
      {
         quest.InstantiateCurrentQuestStep(this.transform);
      }
      else
      {
         ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
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
