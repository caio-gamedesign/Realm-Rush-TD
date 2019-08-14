using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;
    List<Waypoint> path;
    [SerializeField] float tickTime = 3f;
    [SerializeField] ParticleSystem goalParticle;

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
            if(FinishedPath(waypoint))
            {
                SelfDestruct();
            }
            yield return new WaitForSeconds(tickTime);
        }
    }

    private bool FinishedPath(Waypoint waypoint)
    {
        return waypoint == path[path.Count - 1];
    }

    private void SelfDestruct()
    {
        Instantiate(goalParticle, transform.position, Quaternion.identity);
        Destroy(gameObject, .1f);
    }
}
