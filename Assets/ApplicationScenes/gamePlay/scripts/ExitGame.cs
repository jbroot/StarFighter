using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

    public void Start()
    {
        //hide exit options menu by default on start
        CloseExitConfirmationMenu();
        //hide username menu on start
        CloseGetUsernameMenu();
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void GoToHighScoresMenu(){
        SceneManager.LoadScene(2);
    }

    public void CloseExitConfirmationMenu(){
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        if(exitConfirmationMenu != null && exitConfirmationMenu.activeSelf){
            exitConfirmationMenu.SetActive(false);
        }
    }
    public void ShowExitConfirmationMenu(){
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        if (exitConfirmationMenu != null && !exitConfirmationMenu.activeSelf)
        {
            exitConfirmationMenu.SetActive(true);
        }
    }

    public void CloseGetUsernameMenu()
    {
        GameObject unameMenu = GameObject.Find("UsernameMenu");
        if (unameMenu != null && unameMenu.activeSelf == true){
            unameMenu.SetActive(false);
        }
    }

    public void DisplayGetUsernameMenu(){
        GameObject unameMenu = GameObject.Find("UsernameMenu");
        if (unameMenu != null)
        {
            unameMenu.SetActive(true);
            HideErrorMessage();
           // DisableQuitButton();
        }
    }

    public void HideErrorMessage()
    {
        TextMeshProUGUI errorText = GameObject.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        if (errorText != null){
            errorText.SetText("");
        }
    }

    public void DisplayErrorMessage(){
        TextMeshProUGUI errorText = GameObject.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        if(errorText != null)
        {
            errorText.SetText("USERNAME IS REQUIRED");
        }

    }

    public void ValidateUsernameEntered(){
        GameObject usernameInputGO = GameObject.Find("UsernameInputField");
        InputField usernameInput = usernameInputGO.GetComponent<InputField>();
        string uname = usernameInput.text;
        //TODO get score 
        int score = 0;

        if(uname.Length > 0){
            HideErrorMessage();
            //CloseGetUsernameMenu();
            SaveUsernameAndScore(uname, score);
            ShowExitConfirmationMenu();
            DisplayGetUsernameMenu();
            EnableQuitButton();
            GoToHighScoresMenu();
        }
        else{
            DisplayErrorMessage();
        }
    }

    public void EndOfGame(){
        DisableQuitButton();
        DisplayGetUsernameMenu();
    }

    public void EnableQuitButton(){
        GameObject exitBtn = GameObject.Find("Quit");
        Button quitButton = exitBtn.GetComponent<Button>();
        quitButton.enabled = true;
        quitButton.IsActive();
    }

    public void DisableQuitButton()
    {
        GameObject exitBtn = GameObject.Find("Quit");
        Button quitButton = exitBtn.GetComponent<Button>();
        quitButton.enabled = false;
    }

    public void SaveUsernameAndScore(string username, int score){
        //TODO
    }
}
