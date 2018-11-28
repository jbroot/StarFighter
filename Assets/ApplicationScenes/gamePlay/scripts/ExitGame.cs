using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour {

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseExitConfirmationMenu(){
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        //exitConfirmationMenu.SetActive(false);
    }
    public void ShowExitConfirmationMenu()
    {
        GameObject exitConfirmationMenu = GameObject.Find("ExitConfirmationMenu");
        //exitConfirmationMenu.SetActive(true);
    }

}
