using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;

    public void DisplayInLog(string content)
    {
        logText.text = content;
    }
}
