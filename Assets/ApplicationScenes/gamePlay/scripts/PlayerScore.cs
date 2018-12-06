using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int CurrentScore { get; set; }

    private ScoreDictionary scoresDictionary;

	// Use this for initialization
	void Start ()
	{

	    CurrentScore = 0;
        scoresDictionary = new ScoreDictionary();
	}

    public void AddScore(string DestroyedObjectTag)
    {
        CurrentScore += (int) scoresDictionary.scores[DestroyedObjectTag];
        //Debug.Log("Score: " + CurrentScore);
    }

    public int GetScore(){
        return CurrentScore;
    }
}
