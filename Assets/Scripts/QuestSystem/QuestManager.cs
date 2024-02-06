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

      Quest quest = GetQuestById("InitialQuest");
      Debug.Log(quest.info.displayName);
      Debug.Log(quest.info.previousQuestsRequired);
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
}
