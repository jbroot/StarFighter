using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    public int CurrentLives = 0;
    public int StartingLives = 3;

    public Text RemainingLivesText;

	// Use this for initialization
	void Start ()
	{
	    CurrentLives = StartingLives;
	}

    public void LoseALife()
    {
        CurrentLives -= 1;       
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
	    RemainingLivesText.text = "Lives: " + CurrentLives;
	}
}
