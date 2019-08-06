using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    Transform targetEnemy;
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem bullets;

    bool isShooting = false;

    private void Awake()
    {
        SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = attackRange;

        StopShooting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform newEnemy = other.transform;
            if (targetEnemy == null || CloserThanTargetEnemy(newEnemy))
            {
                targetEnemy = newEnemy;
            }
        }
    }

    private bool CloserThanTargetEnemy(Transform newEnemy)
    {
        float distanceNewEnemy = Vector3.Distance(transform.position, newEnemy.position);
        float distanceTargetEnemy = Vector3.Distance(transform.position, targetEnemy.position);

        return distanceNewEnemy < distanceTargetEnemy;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == targetEnemy)
        {
            targetEnemy = null;
        }
    }

    private void Update()
    {
        objectToPan.LookAt(targetEnemy);

        if (targetEnemy && Vector3.Distance(targetEnemy.transform.position, transform.position) <= attackRange)
        {   
            ShootEnemy();
        }
        else if (isShooting)
        {
            StopShooting();
        }
    }

    private void StopShooting()
    {
        var em = bullets.emission;
        em.enabled = false;
        isShooting = false;
    }

    private void ShootEnemy()
    {
        var em = bullets.emission;
        em.enabled = true;
        isShooting = true;
    }
}
