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
            yield return new WaitForSeconds(tickTime);
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
        Instantiate(goalParticle, transform.position, Quaternion.identity);
    }
}
