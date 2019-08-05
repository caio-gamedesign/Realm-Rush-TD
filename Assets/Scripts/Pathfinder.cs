using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            print(waypoint.name);
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
