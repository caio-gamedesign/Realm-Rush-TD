using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int damage = 1;
    [SerializeField] Text healthText;

    [SerializeField] private AudioClip takingDamageAudioClip;
    private AudioSource audioSource;

    private void Start()
    {
        UpdateHealthText();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        health -= damage;
        audioSource.PlayOneShot(takingDamageAudioClip);
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
}
