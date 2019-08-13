using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(1, 10)] private int numOfEnemies = 5;
    [SerializeField] [Range(0f, 10f)] private float secondsBetweenSpawns = 5f;

    [SerializeField] private AudioClip spawnedEnemyAudioClip;
    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator SpawnEnemy()
    {
        while (numOfEnemies > 0)
        { 
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawns);
            audioSource.PlayOneShot(spawnedEnemyAudioClip);
            numOfEnemies--;
        }
    }
}
