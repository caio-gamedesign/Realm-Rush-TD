﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public const int GRID_SIZE = 10;

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(GridPosition(transform.position.x), GridPosition(transform.position.z));
    }

    private int GridPosition(float position)
    {
        return Mathf.RoundToInt(position / GRID_SIZE);
    }
}