using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PathNodeGrid1 : MonoBehaviour
{
    [SerializeField]
    float additionalGridSize = 5f;
    [SerializeField]
    List<Rect> locations = new List<Rect>();
    [SerializeField]
    float step = 0.5f;
    PathNode[,] possibilityMap;

    public int GridWidth { get => possibilityMap.GetLength(0); }
    public int GridHeight { get => possibilityMap.GetLength(1); }
    public static PathNodeGrid1 instance { get; private set; }

    /*void Start()
    {
        instance = this;
        // locations = new List<Rect>();
        // locations.Add(new Rect(new Vector2(-7.5f, -4.5f), new Vector2(15f, 9f)));

        InitKeyPoints();
    }
    public void GetGrid(Vector2 p1, Vector2 p2)
    {
        var minp = new Vector2(Mathf.Min(p1.x, p2.x), Mathf.Min(p1.y, p2.y));
        var wh = new Vector2(Mathf.Abs(p1.x - p2.x) + additionalGridSize, Mathf.Abs(p1.y - p2.y) + additionalGridSize);
        var location = new Rect(minp, wh);
        return InitKeyPoints(location);
    }
    void InitKeyPoints(Rect location)
    {

        var width = (int)(location.width / step);
        var height = (int)(location.height / step);
        var possibilityMap = new PathNode[width, height];
        int gridX, gridY;
        gridX = 0;
        for (float x = location.xMin; x < location.xMax; x += step)
        {
            gridY = 0;
            for (float y = location.yMin; y < location.yMax; y += step)
            {
                possibilityMap[gridX, gridY] = new PathNode(new Vector2(x, y), new Vector2Int(gridX, gridY));
                gridY++;
            }
            gridX++;
        }
        return possibilityMap;
    }

    public void UpdateWalkablePathNodes(float radius)
    {
        for (var intX = 0; intX < possibilityMap.GetLength(0); intX++)
        {
            for (var intY = 0; intY < possibilityMap.GetLength(1); intY++)
            {
                possibilityMap[intX, intY].UpdateWalkable(radius);
            }
        }
    }

    public PathNode GetPathNodeAt(Vector2Int coord)
    {
        return possibilityMap[coord.x, coord.y];
    }

    public PathNode FindClosestPathNode(Vector2 coord)
    {
        var location = locations[0];
        if (coord.x < location.xMin || coord.y < location.yMin ||
            coord.x > location.xMax || coord.y > location.yMax)
        {
            return null;
        }
        var x = (int)((coord.x - location.xMin) / location.width * GridWidth);
        var y = (int)((coord.y - location.yMin) / location.height * GridHeight);
        return possibilityMap[x, y];
    }*/
}
