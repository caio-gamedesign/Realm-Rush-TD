using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid;

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    static readonly Vector2Int[] DIRECTIONS = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    Queue<Waypoint> queue = new Queue<Waypoint>();

    [SerializeField] List<Waypoint> path;

    [SerializeField] Vector2Int[] randomDirections;

    private void CalculatePath()
    {
        path = new List<Waypoint>();

        AddToPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            AddToPath(previous);
            previous = previous.exploredFrom;
        }

        AddToPath(startWaypoint);

        path.Reverse();
    }

    private void AddToPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.DisableTowerPlacement();
    }

    private void QueueNeighbour(Waypoint neighbour, Waypoint exploredFrom)
    {
        if (neighbour.isExplored == false && queue.Contains(neighbour) == false)
        {
            neighbour.SetTopColor(Color.black);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = exploredFrom;
        }
    }

    private void ExploreNeighbours(Waypoint searchCenter)
    {
        randomDirections = RandomDirections();

        //foreach (Vector2Int direction in DIRECTIONS)
        foreach (Vector2Int direction in randomDirections)
        {
            Vector2Int neighboursCoordinates = searchCenter.GetGridPosition() + direction;

            if (grid.TryGetValue(neighboursCoordinates, out Waypoint neighbour))
            {
                QueueNeighbour(neighbour, searchCenter);
            }
        }
    }

    private Vector2Int[] RandomDirections()
    {
        Vector2Int[] randomDirections = new Vector2Int[DIRECTIONS.Length];
        int[] indexes = new int[randomDirections.Length];

        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }

        for (int i = 0; i < randomDirections.Length; i++)
        {
            int randomIndex = 0;
            int index = -1;

            while (index == -1)
            {
                randomIndex = UnityEngine.Random.Range(0, indexes.Length);
                index = indexes[randomIndex];
            }
                        
            indexes[randomIndex] = -1;
            randomDirections[i] = DIRECTIONS[index];
        }

        return randomDirections;
    }

    private void BreadthFirstSearch()
    {
        bool isRunning = true;
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            Waypoint searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;

            if (searchCenter == endWaypoint)
            {
                isRunning = false;
            }
            else
            {
                ExploreNeighbours(searchCenter);
            }
        }
    }

    private bool isWaypointDuplicated(Waypoint waypoint)
    {
        bool isDuplicated = grid.ContainsKey(waypoint.GetGridPosition());

        if (isDuplicated)
        {
            Debug.LogWarning("Duplicated waypoint: " + waypoint.name);
        }

        return isDuplicated;
    }

    private void CreateGrid()
    {
        grid = new Dictionary<Vector2Int, Waypoint>();

        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            if (isWaypointDuplicated(waypoint) == false)
            {
                grid.Add(waypoint.GetGridPosition(), waypoint);
                waypoint.EnableTowerPlacement();
            }
            else
            {
                Destroy(waypoint.gameObject);
            }
        }
    }

    private void CreatePath()
    {
        CreateGrid();
        BreadthFirstSearch();
        CalculatePath();
    }

    private bool isPathEmpty()
    {
        return path.Count <= 0;
    }

    public List<Waypoint> GetPath()
    {
        if (isPathEmpty())
        {
            CreatePath();
        }
        return path;
    }
}
