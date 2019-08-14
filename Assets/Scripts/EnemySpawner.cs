using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(1, 30)] private int numOfEnemies = 5;
    [SerializeField] [Range(0f, 5f)] private float secondsBetweenSpawns = 5f;

    [SerializeField] private AudioClip spawnedEnemyAudioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (numOfEnemies > 0)
        {
            audioSource.PlayOneShot(spawnedEnemyAudioClip);
            Instantiate(enemy, transform.position, Quaternion.identity);
            numOfEnemies--;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
