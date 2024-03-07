 using System;
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
 public string[] questLogMessages;

 private void OnValidate()
 {
  #if UNITY_EDITOR
  id = this.name;
  UnityEditor.EditorUtility.SetDirty(this);
  #endif
 }
}
