﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;
    List<Waypoint> path;
    [SerializeField] float tickTime = 2f;

    private void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(tickTime);
        }
    }
}
