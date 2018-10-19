using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPage : MonoBehaviour {

    // Use this for initialization
	private string u = "username";
	private string p = "password";

	private string usernameString = "Enter username";
	private string passwordString = "Enter Password";

	private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);

	void start()
	{

	}

	void OnGUI()
	{

		GUI.Window(0, windowRect, WindowFunction, "Login");
	}

	void WindowFunction(int windowID)
	{
		//user input log-input
		usernameString = GUI.TextField(new Rect(Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 10), usernameString, 10);
		//input user password
		//passwordString = GUI.PasswordField(new Rect(Screen.width / 3, 2 * Screen.height / 5,Screen.width / 3, Screen.height / 10), passwordString, "*"[0], 10);

		if (GUI.Button(new Rect(Screen.width / 2, 4 * Screen.height / 5, Screen.width / 8, Screen.height / 8), "Log-in"))
		{
			if (usernameString == u && passwordString == p)
			{
				//log-in
				Debug.Log("Welcome friend");

			}
			else
			{
				Debug.Log("Wrong username or password");
			}

			GUI.Label(new Rect(Screen.width / 3, 35 * Screen.height / 100, Screen.width / 5, Screen.height / 8), "Username");
			GUI.Label(new Rect(Screen.width / 3, 62 * Screen.height / 100, Screen.width / 5, Screen.height / 8), "Password");

		}
	}
}


