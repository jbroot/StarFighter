using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class top10 : MonoBehaviour
{
    public Text namesBox;
    public string URL = "http://spacefighterweb.azurewebsites.net/api/values";
    public Text responseText;

	// Use this for initialization
	void Start()
    {
        namesBox.text = "";
        Request();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Request()
    {
        WWW request = new WWW(URL);
        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;
        responseText.text = req.text;
        namesBox.text = req.text;
    }
}
