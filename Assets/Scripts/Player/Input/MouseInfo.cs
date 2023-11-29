using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickAction { Click, DoubleClick, None };
public enum MouseButton { Left = 0, Right, Middle, None };
public class MouseInfo
{
    ClickAction clickAction;
    MouseButton mouseButton;
    Vector3 mousePosition;
    public MouseInfo(ClickAction clickAction, MouseButton mouseButton, Vector3 mousePosition)
    {
        this.clickAction = clickAction;
        this.mouseButton = mouseButton;
        this.mousePosition = mousePosition;
    }
    public MouseInfo(MouseInfo other)
    {
        clickAction = other.clickAction;
        mouseButton = other.mouseButton;
        mousePosition = other.mousePosition;
    }
    public MouseInfo copy() {
        return new MouseInfo(this);
    }
    public ClickAction ClickAction { get => clickAction; set => clickAction = value; }
    public MouseButton MouseButton { get => mouseButton; set => mouseButton = value; }
    public Vector3 MousePosition { get => mousePosition; set => mousePosition = value; }
}
