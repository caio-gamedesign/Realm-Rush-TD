using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint start, end;

    Vector2Int[] directions = {
        Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
    };

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = start.GetGridPosition() + direction;
            if (grid.ContainsKey(explorationCoordinates))
            {
                grid[explorationCoordinates].SetTopColor(Color.white);
            }
        }
    }

    private void ColorStartAndEnd()
    {
        start.SetTopColor(Color.black);
        end.SetTopColor(Color.gray);
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
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
