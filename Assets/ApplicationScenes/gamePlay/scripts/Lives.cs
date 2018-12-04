using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public int CurrentLives = 0;
    public int StartingLives = 3;

    public Text RemainingLivesText;

	// Use this for initialization
	void Start ()
	{
	    CurrentLives = StartingLives;
        gameObject.AddComponent<AudioSource>();
        source.clip = explosionSound;
        source.playOnAwake = true;
    }

    void playExplosionSound()
    {
        source.PlayOneShot(explosionSound);
    }

    public void LoseALife()
    {
        CurrentLives -= 1;
        playExplosionSound();
        if (CurrentLives < 0)
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<Health>().Respawn();
    }

    public void GainALife()
    {
        CurrentLives += 1;
    }

	// Update is called once per frame
	void Update ()
	{
		if (RemainingLivesText != null)
		{
            RemainingLivesText.text = "Lives: " + CurrentLives;
		}
    }
}
