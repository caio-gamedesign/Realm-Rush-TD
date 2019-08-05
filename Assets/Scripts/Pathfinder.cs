using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint start, end;

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
    }

    private void Pathfind()
    {
        queue.Enqueue(start);

        while (queue.Count > 0 && isRunning)
        {
            Waypoint searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
        }


    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == end)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (isRunning == false)
        {
            return;
        }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighboursCoordinates = from.GetGridPosition() + direction;
            if (grid.ContainsKey(neighboursCoordinates))
            {
                QueueNeighbour(neighboursCoordinates);
            }
        }
    }

    private void QueueNeighbour(Vector2Int neighboursCoordinates)
    {
        Waypoint neighbour = grid[neighboursCoordinates];

        if (neighbour.isExplored == false)
        {
            neighbour.SetTopColor(Color.black);
            queue.Enqueue(neighbour);
        }
    }

    private void ColorStartAndEnd()
    {
        start.SetTopColor(Color.white);
        end.SetTopColor(Color.white);
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPosition = waypoint.GetGridPosition();
            bool isOverlapping = grid.ContainsKey(gridPosition);
            if (isOverlapping)
            {
                Debug.LogWarning("Deleted Overlapping block " + waypoint + " with " + grid[gridPosition]);
                Destroy(waypoint.gameObject);
            }
            else
            {
                grid.Add(gridPosition, waypoint);
            }
        }
    }
}
