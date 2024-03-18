using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathNodeGrid : MonoBehaviour
{
    List<Rect> locations;
    [SerializeField]
    float step;
    [SerializeField]
    float radius;
    PathNode[,] possibilityMap;

    public int GridWidth { get => possibilityMap.GetLength(0); }
    public int GridHeight { get => possibilityMap.GetLength(1); }
    public static PathNodeGrid instance { get; private set; }
    
    void Start()
    {
        instance = this;
        locations = new List<Rect>();
        locations.Add(new Rect(new Vector2(0, 0), new Vector2(4.5f, 2.5f)));

        InitKeyPoints();
        UpdateWalkablePathNodes();
    }

    void InitKeyPoints()
    {
        /*var areas = locations.Select(area => area.width * area.height);
        var totalArea = areas.Sum();
        var areaPercentage = areas.Select(area => area / totalArea).ToArray();*/
        foreach (var location in locations)
        {
            var width = (int)(location.width / step);
            var height = (int)(location.height / step);
            possibilityMap = new PathNode[width, height];
            int gridX, gridY;
            gridX = 0;
            for (float x = location.xMin; x < location.xMax; x += step)
            {
                gridY = 0;
                for (float y = location.yMin; y < location.yMax; y += step)
                {
                    possibilityMap[gridX, gridY] = new PathNode(true, new Vector2(x, y), new Vector2Int(gridX, gridY));
                }
            }
        }
    }

    public void UpdateWalkablePathNodes()
    {
        for (var intX = 0; intX < possibilityMap.GetLength(0); intX++)
        {
            for (var intY = 0; intY < possibilityMap.GetLength(1); intY++)
            {
                possibilityMap[intX, intY].UpdateWalkable(radius);
            }
        }
    }

    public void LockPathNode(PathNode node)
    {
        node.walkable = false;
    }
    public PathNode GetPathNodeAt(Vector2Int coord)
    {
        return possibilityMap[coord.x, coord.y];
    }

    public PathNode FindClosestPathNode(Vector2 coord)
    {
        var location = locations[0];
        if(coord.x < location.xMin || coord.y < location.yMin ||
            coord.x > location.xMax || coord.y > location.yMax)
        {
            return null;
        }
        var x = (int)((coord.x - location.xMin) / location.width * GridWidth);
        var y = (int)((coord.y - location.yMin) / location.height * GridHeight);
        return possibilityMap[x, y];
    }
}
