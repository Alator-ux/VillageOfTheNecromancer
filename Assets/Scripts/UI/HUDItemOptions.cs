using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDItemOptions : MonoBehaviour
{
    public enum MenuAction { Use, Drop, DropAll, Cancel};
    [SerializeField]
    private GameObject useButton, dropButton, dropAllButton, cancelButton;

    public void SetOnButtonClickCallback(Action<MenuAction> callback)
    {
        useButton.GetComponent<Button>().onClick.AddListener(() => {
            callback(MenuAction.Use);
            Destroy(gameObject);
        });
        dropButton.GetComponent<Button>().onClick.AddListener(() => {
            callback(MenuAction.Drop);
            Destroy(gameObject);
        });
        dropAllButton.GetComponent<Button>().onClick.AddListener(() => {
            callback(MenuAction.DropAll);
            Destroy(gameObject);
        });
        cancelButton.GetComponent<Button>().onClick.AddListener(() => {
            callback(MenuAction.Cancel);
            Destroy(gameObject);
        });
    }
}
