using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;  

public class top10 : MonoBehaviour {

    private List<Score> scoresInOrder = new List<Score>();

	// Use this for initialization
    public void Start() {
        AddDummyDataToHighScores(); //TODO: Replace with data retrieved from the database
        SortScoresHighestToLowest();
        DisplayTopTenHighestScoresNames();
        DisplayTopTenHighestScores();
    }

    public void DisplayTopTenHighestScores(){
        Text scoresBox = GameObject.Find("scoresBox").GetComponent<Text>();
        scoresBox.text = "";
        for (int i = 0; i < 10; i++)
        {
            Score s = (Score)scoresInOrder[i];
            int score = s.TotalPoints;
            scoresBox.text += score.ToString() + "\n";
        }
    }

    public void DisplayTopTenHighestScoresNames()
    {
        Text namesBox = GameObject.Find("namesBox").GetComponent<Text>();
        namesBox.text = "";
        for (int i = 0; i < 10; i++)
        {
            Score s = (Score)scoresInOrder[i];
            string username = s.Username;
            namesBox.text += username + "\n";
        }
    }

    private void AddDummyDataToHighScores(){
        string[] names = new string[10]{"Zeus", "Muhammad", "Washington", "Jo", "Jane", "Jax", "noob123",
        "Orange Man", "Cheater", "King Kong" };
        int[] scores = new int[10] { 1000, 30000, 500, 70, 8000, 600000, 0, 7770, 990, 10 };

        for (int i = 0; i < 10; i++){
            Score s = new Score(names[i], scores[i]);
            scoresInOrder.Add(s);
        }

    }

    private void SortScoresHighestToLowest(){
        scoresInOrder.Sort();
        scoresInOrder.Reverse();
    }

    public void AddNewScore(string username, int score){
        Score s = new Score(username, score);
        scoresInOrder.Add(s);
    } 
}
