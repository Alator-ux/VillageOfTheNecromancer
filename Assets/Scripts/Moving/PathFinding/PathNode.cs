using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathNode 
{
    public bool walkable;
    public Vector2 worldPosition;
    public Vector2Int gridPosition;

    private PathNode parentNode = null;
    private PathNode childNode = null;

    public PathNode ParentNode
    {
        get => parentNode;
        set => SetParent(value);
    }

    private float distance = float.PositiveInfinity;  //  расстояние от начальной вершины

    public float Distance
    {
        get => distance;
        set => distance = value;
    }
    public PathNode ChildNode { get => childNode; set => childNode = value; }

    private void SetParent(PathNode parent)
    {
        parentNode = parent;
        if (parent == null)
        {
            distance = float.PositiveInfinity;
        }
    }

    public PathNode(bool _walkable, Vector2 position, Vector2Int gridPosition)
    {
        this.walkable = _walkable;
        this.worldPosition = position;
        this.gridPosition = gridPosition;
    }

    public static float Dist(PathNode a, PathNode b)
    {
        return Vector2.Distance(a.worldPosition, b.worldPosition);
    }

    public bool UpdateWalkable(float radius)
    {
        var collider = Physics2D.OverlapCircle(worldPosition, radius);
        walkable = collider == null || collider.isTrigger;
        return walkable;
    }

    // TODO: equals? + soft equal
    public bool Equal(PathNode other)
    {
        return worldPosition.Equals(other.worldPosition);
    }
}
