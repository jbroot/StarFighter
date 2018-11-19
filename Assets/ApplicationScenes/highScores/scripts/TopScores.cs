using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TopScores : MonoBehaviour {
    private string URL = "http://spacefighterweb.azurewebsites.net/api/scores";
    private List<Score> scoresList = new List<Score>();

	// Use this for initialization
    public void Start()
    {
        StartCoroutine(GetScores());
    }

    public IEnumerator GetScores()
    {
        UnityWebRequest webapi = UnityWebRequest.Get(URL);
        yield return webapi.SendWebRequest();

        if (webapi.isNetworkError || webapi.isHttpError)
        {
            Debug.Log(webapi.error);
        }
        else
        {
            LoadScoresList(webapi.downloadHandler.text);
            DisplayScores();
        }
    }

    public void LoadScoresList(string data)
    {
        var trimmedString = data.Substring(1, data.Length-2);
        var elements = trimmedString.Split(',');
        for (int n = 0; n < elements.Length; n += 2)
        {
            scoresList.Add(new Score(elements[n].Replace("\"", " "), int.Parse(elements[n + 1].Replace("\"", " "))));
        }
    }

    public void DisplayScores()
    {
        var scoresObj = new GameObject();
        scoresObj = GameObject.Find("scoresText");
        scoresObj.GetComponent<UnityEngine.UI.Text>().text = "";
        foreach (var s in scoresList)
        {
            var line = string.Format("{0,35}           {1,-10}\n", s.Username.ToString(), s.TotalPoints);
            scoresObj.GetComponent<UnityEngine.UI.Text>().text += line;
        }
    }
}
