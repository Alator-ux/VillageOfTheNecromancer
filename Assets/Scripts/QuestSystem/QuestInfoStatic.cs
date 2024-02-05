 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu (fileName = "QuestInfoStatic", menuName = "ScriptableObject/QuestInfo", order = 1)]
public class QuestInfoStatic : ScriptableObject
{
 [SerializeField] public string id { get; private set; }

 public string displayName;

 public QuestInfoStatic[] previousQuestsRequired;

 public GameObject[] questStepsPrefabs;

}
