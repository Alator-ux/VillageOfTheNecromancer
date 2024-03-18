using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    PathNodeGrid grid;
    void Start()
    {
        grid = PathNodeGrid.instance;
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

    List<Tuple<PathNode, PathNode>> AStarPath(PathNode start, PathNode finish)
    {
        var pq = new PriorityQueue<Tuple<Vector2Int, float>>((grid.GridWidth + grid.GridHeight) * 2);
        pq.Enqueue(new Tuple<Vector2Int, float>(start.gridPosition, 0), 0);

        var family = new Dictionary<PathNode, PathNode>();
        family[start] = null;

        Vector2Int currentNode;
        float currentNodeDist;
        while (!pq.IsEmpty)
        {
            {
                var tuple = pq.Dequeue();
                currentNode = tuple.Item1.Item1;
                currentNodeDist = tuple.Item1.Item2;
            }
            if (currentNode == finish.gridPosition)
            {
                break;
            }
            if (grid.GetPathNodeAt(currentNode).Distance < currentNodeDist)
            {
                continue;
            }
            var currentPathNode = grid.GetPathNodeAt(currentNode);
            var neighbours = GetNeighbours(currentNode);
            foreach (var neighbourNode in neighbours)
            {
                var neighbourPathNode = grid.GetPathNodeAt(neighbourNode);
                if (neighbourPathNode.walkable)
                {
                    var newNodeDistance = currentNodeDist + 
                        PathNode.Dist(neighbourPathNode, currentPathNode);
                    if (newNodeDistance < neighbourPathNode.Distance)
                    {
                        neighbourPathNode.Distance = newNodeDistance;
                        //family[currentPathNode] = new Tuple<PathNode, PathNode>(family[currentPathNode].Item1, neighbourPathNode);
                        //family[neighbourPathNode] = new Tuple<PathNode, PathNode>(currentPathNode, null);
                        //neighbourPathNode.ParentNode = currentPathNode;
                        //currentPathNode.ChildNode = neighbourPathNode;
                        family[currentPathNode] = neighbourPathNode;
                        family[neighbourPathNode] = null;
                        var expectedFullDistance = newNodeDistance + PathNode.Dist(neighbourPathNode, finish);
                        pq.Enqueue(new Tuple<Vector2Int, float>(neighbourNode, newNodeDistance), expectedFullDistance);
                    }
                }
            }
        }

        var path = new List<Tuple<PathNode, PathNode>>();
        PathNode current = start;
        while (current != null)
        {
            var next = family[current];
            path.Add(new Tuple<PathNode, PathNode>(current, next));
            current = next;
        }

        return path[path.Count - 1].Item1.Equal(finish) ? path : null;
    }

    public void FindPath(Vector2 start, Vector2 finish)
    {
        grid.UpdateWalkablePathNodes();
        var startPathNode = grid.FindClosestPathNode(start);
        var finishPathNode = grid.FindClosestPathNode(finish);
        var path = AStarPath(startPathNode, finishPathNode);
    }
}
