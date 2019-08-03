using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> path;

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Transform waypoint in path)
        {
            transform.position = new Vector3(waypoint.position.x, transform.position.y, waypoint.position.z);
            yield return new WaitForSeconds(1f);
        }
    }
}
