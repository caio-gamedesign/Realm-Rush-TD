using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    Transform currentTarget;
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem bullets;

    [SerializeField] private List<Transform> targets;

    private bool isShooting = false;

    public Waypoint waypoint;

    private void Awake()
    {
        SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = attackRange;

        Refresh();
    }

    public void Refresh()
    {
        targets = new List<Transform>();

        StopShooting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemy = other.transform;
            targets.Add(enemy);
        }
    }

    private bool CloserThanTargetEnemy(Transform newEnemy)
    {
        float distanceNewEnemy = Vector3.Distance(transform.position, newEnemy.position);
        float distanceTargetEnemy = Vector3.Distance(transform.position, currentTarget.position);

        return distanceNewEnemy < distanceTargetEnemy;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemy = other.transform;
            targets.Remove(enemy);
        }
    }

    private void Update()
    {
        SetCurrentTarget();

        if (currentTarget)
        {
            StartShooting();
        }
        else if(isShooting)
        {
            StopShooting();
        }
    }

    private void RemoveDeadTargets()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
            }
        }
    }

    private void SetCurrentTarget()
    {
        RemoveDeadTargets();
        currentTarget = (targets.Count <= 0) ? null : targets[0];
    }

    private void StopShooting()
    {
        var em = bullets.emission;
        em.enabled = false;

        objectToPan.rotation = Quaternion.identity;
        isShooting = false;
    }

    private void StartShooting()
    {
        var em = bullets.emission;
        em.enabled = true;

        objectToPan.LookAt(currentTarget);

        isShooting = true;
    }
}
