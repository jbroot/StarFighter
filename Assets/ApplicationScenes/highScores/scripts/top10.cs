using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;

public class top10 : MonoBehaviour
{
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
        var trimmedString = data.Substring(1, data.Length - 2);
        var elements = trimmedString.Split(',');
        for (int n = 0; n < elements.Length; n += 2)
        {
            scoresList.Add(new Score(elements[n].Replace("\"", " "), int.Parse(elements[n + 1].Replace("\"", " "))));
        }
    }

    public void DisplayScores()
    {
        TextMeshProUGUI namesBox = GameObject.Find("namesBox").GetComponent<TextMeshProUGUI>();
        namesBox.text = "";
        TextMeshProUGUI scoresBox = GameObject.Find("scoresBox").GetComponent<TextMeshProUGUI>();
        scoresBox.text = "";
        foreach (var s in scoresList)
        {
            namesBox.text += string.Format("{0}\n", s.Username);
            scoresBox.text += string.Format("{0}\n", s.TotalPoints);
        }
    }
}
