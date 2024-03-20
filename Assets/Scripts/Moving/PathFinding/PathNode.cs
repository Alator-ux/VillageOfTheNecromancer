using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PathNode 
{
    public Vector2 worldPosition;
    public Vector2Int gridPosition;

    Dictionary<float, bool> walkableForRadius;

    private float distance = float.PositiveInfinity;  //  расстояние от начальной вершины

    public float Distance
    {
        get => distance;
        set => distance = value;
    }

    public PathNode(Vector2 position, Vector2Int gridPosition)
    {
        this.worldPosition = position;
        this.gridPosition = gridPosition;
        walkableForRadius = new Dictionary<float, bool>();
    }

    public static float Dist(PathNode a, PathNode b)
    {
        return Vector2.Distance(a.worldPosition, b.worldPosition);
    }

    public bool IsWalkable(float radius)
    {
        if (walkableForRadius.ContainsKey(radius))
        {
            return walkableForRadius[radius];
        }

        return UpdateWalkable(radius);
    }

    public bool UpdateWalkable(float radius)
    {
        var colliders = Physics2D.OverlapCircleAll(worldPosition, radius);
        var walkable = true;
        foreach(var collider in colliders)
        {
            if (!collider.isTrigger)
            {
                walkable = false;
                break;
            }
        }
        walkableForRadius[radius] = walkable;
        return walkable;
    }

    // TODO: equals? + soft equal
    public bool Equal(PathNode other)
    {
        return worldPosition.Equals(other.worldPosition);
    }
}
