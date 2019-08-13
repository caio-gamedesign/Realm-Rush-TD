using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem deathParticle;

    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathAudioClip, Camera.main.transform.position);

        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        hitPoints--;
        hitParticle.Play();
        audioSource.PlayOneShot(hitAudioClip);
    }
}
