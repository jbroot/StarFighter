using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void GoToHighScoresMenu(){
        SceneManager.LoadScene(2);
    }

    public void CloseExitConfirmationMenu(){
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        exitConfirmationMenu.SetActive(false);
    }
    public void ShowExitConfirmationMenu(){
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        exitConfirmationMenu.SetActive(true);
    }

    public void DisplayGetUsernameMenu(){
        GameObject unameMenu = GameObject.Find("EnterUsername");
        unameMenu.SetActive(true);
        HideErrorMessage();
    }

    public void HideErrorMessage(){
        TextMeshProUGUI errorText = GameObject.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        errorText.enabled = false;
        errorText.text = "";
    }

    public void DisplayErrorMessage(){
        TextMeshProUGUI errorText = GameObject.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        errorText.enabled = true;
        //errorText.text = "";
    }

    public void ValidateUsernameEntered(){
        InputField usernameInput = GetComponent<InputField>();
        string uname = usernameInput.text;

        if(uname.Length > 0){
            HideErrorMessage();
            CloseGetUsernameMenu();
            SaveUsernameAndScore();
            GoToHighScoresMenu();
        }
        else{
            DisplayErrorMessage();
        }
    }

    public void CloseGetUsernameMenu(){
        GameObject unameMenu = GameObject.Find("EnterUsername");
        unameMenu.SetActive(false);
    }


    public void SaveUsernameAndScore(){
        //TODO
    }
}
