using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    public float doubleClickTimeLimit = 0.4f;
    private ClickAction lastClickAction = ClickAction.None;
    private MouseButton lastButton = MouseButton.None;
    private Vector3 globalMousePosition = new Vector3();

    void Start()
    {
        StartCoroutine(InputListener());
    }

    IEnumerator InputListener()
    {
        while (enabled)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                lastClickAction = ClickAction.None;
                lastButton = MouseButton.Left;
                globalMousePosition = Input.mousePosition;
                yield return ClickEvent(lastButton);
            }

            if (Input.GetMouseButtonDown((int)MouseButton.Right))
            {
                lastClickAction = ClickAction.None;
                lastButton = MouseButton.Right;
                globalMousePosition = Input.mousePosition;
                yield return ClickEvent(lastButton);
            }

            yield return null;
        }
    }

    IEnumerator ClickEvent(MouseButton button)
    {
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < doubleClickTimeLimit)
        {
            if (CheckButtonPress(MouseButton.Left, button))
            {
                yield break;
            }
            if (CheckButtonPress(MouseButton.Right, button))
            {
                yield break;
            }
            if (CheckButtonPress(MouseButton.Middle, button))
            {
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }

        lastClickAction = ClickAction.Click;
    }
    bool CheckButtonPress(MouseButton buttonToCheck, MouseButton buttonToCompare)
    {
        bool buttonPressed = Input.GetMouseButtonDown((int)buttonToCheck);
        if(buttonPressed)
        {
            lastClickAction = buttonToCheck == buttonToCompare ? ClickAction.DoubleClick : ClickAction.Click;
        }
        return buttonPressed;
    }
    void ResetButtonNAction()
    {
        lastClickAction = ClickAction.None;
        lastButton = MouseButton.None;
    }
    public MouseInfo ClickButtonNAction()
    {
        Vector3 localMousePosition = Camera.main.ScreenToWorldPoint(globalMousePosition);
        var info = new MouseInfo(lastClickAction, lastButton, globalMousePosition);
        if(lastClickAction != ClickAction.None)
        {
            ResetButtonNAction();
        }
        return info;
    }

    public List<GameObject> ClickedGameObjects(Vector3 mousePosition, LayerMask layer)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            return raycastResults.ConvertAll<GameObject>(raycastObject => raycastObject.gameObject);
        }

        return new List<GameObject>();
    }
}