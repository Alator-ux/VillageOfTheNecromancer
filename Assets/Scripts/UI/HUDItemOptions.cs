using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDItemOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject useButton, dropButton, dropAllButton, cancelButton;

    public void SetOnButtonClickCallback(Action<int> callback)
    {
        useButton.GetComponent<Button>().onClick.AddListener(delegate () {
            callback(0);
            Destroy(gameObject);
        });
        dropButton.GetComponent<Button>().onClick.AddListener(delegate () {
            callback(1);
            Destroy(gameObject);
        });
        dropAllButton.GetComponent<Button>().onClick.AddListener(delegate () {
            callback(2);
            Destroy(gameObject);
        });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate () {
            callback(3);
            Destroy(gameObject);
        });
    }
}
