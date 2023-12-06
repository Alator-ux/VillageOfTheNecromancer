using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickAction { Click, DoubleClick, None };
public enum MouseButton { Left = 0, Right, Middle, None };
public class MouseInfo
{
    ClickAction clickAction;
    MouseButton clickButton;
    Vector3 clickPosition;
    bool hover;
    Vector3 position;
    public MouseInfo(ClickAction clickAction, MouseButton clickButton, Vector3 clickPosition, 
        bool hover, Vector3 position)
    {
        this.clickAction = clickAction;
        this.clickButton = clickButton;
        this.clickPosition = clickPosition;
        this.hover = hover;
        this.position = position;
    }
    public MouseInfo(MouseInfo other)
    {
        clickAction = other.clickAction;
        clickButton = other.clickButton;
        clickPosition = other.clickPosition;
        hover = other.hover;
        position = other.position;
    }
    public MouseInfo copy() {
        return new MouseInfo(this);
    }
    public ClickAction ClickAction { get => clickAction; set => clickAction = value; }
    public MouseButton ClickButton { get => clickButton; set => clickButton = value; }
    public Vector3 ClickPosition { get => clickPosition; set => clickPosition = value; }
    public bool Hover { get => hover; set => hover = value; }
    public Vector3 Position { get => position; set => position = value; }
}
