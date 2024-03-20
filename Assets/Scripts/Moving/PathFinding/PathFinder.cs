using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PathFinder
{
    PathNodeGrid grid;
    float radius;
    public PathFinder(float radius = 0.5f)
    {
        grid = PathNodeGrid.instance;
        this.radius = radius;
    }

    private List<Vector2Int> GetNeighbours(Vector2Int current)
    {
        List<Vector2Int> nodes = new List<Vector2Int>();
        for (int x = current.x - 1; x <= current.x + 1; ++x)
            for (int y = current.y - 1; y <= current.y + 1; ++y)
                if (x >= 0 && y >= 0 && x < grid.GridWidth && y < grid.GridHeight && (x != current.x || y != current.y))
                    nodes.Add(new Vector2Int(x, y));
        return nodes;
    }

    List<Vector2> AStarPath(PathNode start, PathNode finish)
    {
        var pq = new PriorityQueue<Tuple<Vector2Int, float>>((grid.GridWidth + grid.GridHeight) * 2);
        pq.Enqueue(new Tuple<Vector2Int, float>(start.gridPosition, 0), 0);

        var familyBush = new Dictionary<PathNode, PathNode>();
        familyBush[start] = null;

        var minDist = new Dictionary<PathNode, float>();
        minDist[start] = 0;

        Vector2Int currentNode;
        float currentNodeDist, currentExpectedFullDistance;
        while (!pq.IsEmpty)
        {
            {
                var tuple = pq.Dequeue();
                currentNode = tuple.Item1.Item1;
                currentNodeDist = tuple.Item1.Item2;
                currentExpectedFullDistance = tuple.Item2;
            }
            if (currentNode == finish.gridPosition)
            {
                break;
            }
            var currentPathNode = grid.GetPathNodeAt(currentNode);
            if (minDist[currentPathNode] < currentNodeDist)
            {
                continue;
            }
            var neighbours = GetNeighbours(currentNode);
            foreach (var neighbourNode in neighbours)
            {
                var neighbourPathNode = grid.GetPathNodeAt(neighbourNode);
                if (neighbourPathNode.IsWalkable(radius))
                {
                    var newNodeDistance = currentNodeDist + 
                        PathNode.Dist(neighbourPathNode, currentPathNode);
                    var oldDistance = minDist.ContainsKey(neighbourPathNode) ? minDist[neighbourPathNode] : float.PositiveInfinity;
                    if (newNodeDistance < oldDistance)
                    {
                        //neighbourPathNode.Distance = newNodeDistance;
                        //family[currentPathNode] = new Tuple<PathNode, PathNode>(family[currentPathNode].Item1, neighbourPathNode);
                        //family[neighbourPathNode] = new Tuple<PathNode, PathNode>(currentPathNode, null);
                        //neighbourPathNode.ParentNode = currentPathNode;
                        //currentPathNode.ChildNode = neighbourPathNode;
                        familyBush[neighbourPathNode] = currentPathNode;
                        minDist[neighbourPathNode] = newNodeDistance;
                        var expectedFullDistance = newNodeDistance + PathNode.Dist(neighbourPathNode, finish);
                        pq.Enqueue(new Tuple<Vector2Int, float>(neighbourNode, newNodeDistance), expectedFullDistance);
                    }
                }
            }
        }

        var path = new List<Vector2>();
        PathNode current = finish;
        while (current != null && familyBush.ContainsKey(current))
        {
            var prev = familyBush[current];
            path.Insert(0, current.worldPosition);
            current = prev;
        }
        if (path.Count < 1 || !(path[0] == start.worldPosition))
        {
            return null;
        }
        return path.ToList();
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 finish)
    {
        grid.UpdateWalkablePathNodes(radius);
        var startPathNode = grid.FindClosestPathNode(start);
        if(startPathNode == null)
        {
            return null;
        }
        var finishPathNode = grid.FindClosestPathNode(finish);
        if (!finishPathNode.IsWalkable(radius))
        {
            return null;
        }
        return AStarPath(startPathNode, finishPathNode);
    }
}
