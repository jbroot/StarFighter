using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class top10 : MonoBehaviour {

    string[] top10Array = new string[10]{"Zeus", "Muhammad", "Washington", "Jo", "Jane", "Jax", "noob123",
        "Orange Man", "Cheater", "King Kong" };
    int[] scores = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public Text namesBox;
    public Text scoresBox;

	// Use this for initialization
	void Start () {
        namesBox.text = scoresBox.text = "";
        for (int i = 0; i < 10; i++)
        {
            namesBox.text += (i + 1).ToString() + ". " + top10Array[i] + "\n";
            scoresBox.text += scores[i].ToString() + "\n";
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
