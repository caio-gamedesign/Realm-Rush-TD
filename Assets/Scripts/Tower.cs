using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem bullets;

    bool isShooting = false;


    private void LateUpdate()
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
