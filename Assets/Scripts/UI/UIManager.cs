using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hud, craftPanel;

    private HUDInventoryInputScript hudInventoryInputScript;
    private CraftPanelScript craftPanelScript;

    bool hudIsInteractive;

    public bool HandleInput { get => hudIsInteractive; }

    private void Start()
    {
        hudInventoryInputScript = gameObject.transform.Find("HUD").GetComponent<HUDInventoryInputScript>();

        craftPanelScript = gameObject.transform.Find("CraftPanel").GetComponent<CraftPanelScript>();
        craftPanelScript.OnDisableCallback = () =>
        {
            hudIsInteractive = true;
            Time.timeScale = 1f;
        };
    }
    
    public void ProcessMouseInput(MouseInfo mouseInfo)
    {
        if (hudIsInteractive)
        {
            hudInventoryInputScript.ProcessMouseInput(mouseInfo);
        }
    }
    public void ProcessNumInput(KeyCode keyCode)
    {
        if (hudIsInteractive)
        {
            hudInventoryInputScript.ProcessNumInput(keyCode);
        }
    }
    public void ProcessCraftButtonInput()
    {
        if (hudIsInteractive)
        {
            hudIsInteractive = false;
            Time.timeScale = 0f;
            craftPanelScript.SetActive();
        }
        else
        {
            craftPanelScript.SetInactive();
        }
    }
    
}
