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
	}

    public void AddScore(string DestroyedObjectName)
    {
        CurrentScore += (int) scoresDictionary.scores[DestroyedObjectName];
    }

    public int GetScore(){
        return CurrentScore;
    }
}
