using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        UpdateUIScore(CurrentScore);
    }

    public int GetScore(){
        return CurrentScore;
    }

    public void UpdateUIScore(int currentScore){
        TextMeshProUGUI scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.text = currentScore.ToString();
    }
}
