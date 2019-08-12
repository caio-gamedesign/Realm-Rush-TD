﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid;

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    static readonly Vector2Int[] DIRECTIONS = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Waypoint searchCenter;

    Waypoint[] waypoints;

    [SerializeField] List<Waypoint> path;

    public List<Waypoint> GetPath()
    {
        if (isPathCreated() == false)
        {
            CreatePath();
        }
        return path;
    }

    private bool isPathCreated()
    {
        return path.Count > 0;
    }

    private void CreatePath()
    {
        CreateGrid();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CalculatePath();
    }

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

    private void QueueNeighbour(Waypoint neighbour)
    {
        if (neighbour.isExplored == false && queue.Contains(neighbour) == false)
        {
            neighbour.SetTopColor(Color.black);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) return;

        foreach (Vector2Int direction in DIRECTIONS)
        {
            Vector2Int neighboursCoordinates = searchCenter.GetGridPosition() + direction;

            if (grid.TryGetValue(neighboursCoordinates, out Waypoint neighbour))
            {
                QueueNeighbour(neighbour);
            }
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void SetSearchCenter(Waypoint waypoint)
    {
        searchCenter = waypoint;
        searchCenter.isExplored = true;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            SetSearchCenter(queue.Dequeue());
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.white);
        endWaypoint.SetTopColor(Color.white);
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

        DeleteRandomWaypoints(Mathf.RoundToInt(transform.childCount/10));

        waypoints = GetComponentsInChildren<Waypoint>();

        startWaypoint = waypoints[UnityEngine.Random.Range(0, waypoints.Length)];

        while (endWaypoint == null || endWaypoint == startWaypoint)
        {
            endWaypoint = waypoints[UnityEngine.Random.Range(0, waypoints.Length)];
        }

        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPosition = waypoint.GetGridPosition();
            if (isWaypointDuplicated(waypoint) == false)
            {
                grid.Add(gridPosition, waypoint);
                waypoint.EnableTowerPlacement();
            }
            else
            {
                Destroy(waypoint.gameObject);
            }
        }
    }

    private void DeleteRandomWaypoints(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject childToDelete = transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).gameObject;
            DestroyImmediate(childToDelete);
        }
    }
}
