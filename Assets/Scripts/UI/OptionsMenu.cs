using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonsObjects;
    protected Button[] buttons;
    protected void Awake()
    {
        buttons = new Button[buttonsObjects.Length];
        for (var i = 0; i < buttonsObjects.Length; i++)
        {
            buttons[i] = buttonsObjects[i].GetComponent<Button>();
        }
    }
    public void SetOnButtonClickCallback(Action callback)
    {
        for (var i = 0; i < buttonsObjects.Length; i++)
        {
            buttons[i].onClick.AddListener(() => {
                callback();
            });
        }
    }
}
