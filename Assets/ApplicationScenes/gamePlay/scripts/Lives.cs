using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource Source { get { return GetComponent<AudioSource>(); } }

    public int CurrentLives = 0;
    public int StartingLives = 3;

    public Text RemainingLivesText;

	// Use this for initialization
	void Start ()
	{
	    CurrentLives = StartingLives;
        gameObject.AddComponent<AudioSource>();
        Source.clip = explosionSound;
        Source.playOnAwake = true;
    }

    void PlayExplosionSound()
    {
        Source.PlayOneShot(explosionSound);
    }

    public void LoseALife()
    {
        CurrentLives -= 1;
        PlayExplosionSound();
        UpdateLivesUI();
        if (CurrentLives <= 0)
        {
            ExitGame exit = new ExitGame();
            exit.DisplayGetUsernameMenu();
            Destroy(gameObject);
        }
        gameObject.GetComponent<Health>().Respawn();
    }

    public void GainALife()
    {
        CurrentLives += 1;
        UpdateLivesUI();
    }

	// Update is called once per frame
	public void UpdateLivesUI()
	{
		if (RemainingLivesText != null)
		{
            RemainingLivesText.text = "Lives: " + CurrentLives;
		}
        GameObject life1imgGO = GameObject.Find("life1");
        Image life1img = life1imgGO.GetComponent<Image>();
        GameObject life2imgGO = GameObject.Find("life2");
        Image life2img = life2imgGO.GetComponent<Image>();
        GameObject life3imgGO = GameObject.Find("life3");
        Image life3img = life3imgGO.GetComponent<Image>();

        switch (CurrentLives){
            case 3:
                life1img.enabled = true;
                life2img.enabled = true;
                life3img.enabled = true;
                break;
            case 2:
                life1img.enabled = true;
                life2img.enabled = true;
                life3img.enabled = false;
                break;
            case 1:
                life1img.enabled = true;
                life2img.enabled = false;
                life3img.enabled = false;
                break;
            case 0:
                life1img.enabled = false;
                life2img.enabled = false;
                life3img.enabled = false;
                GameObject gameOverBtn = GameObject.Find("gameoverInvisible");
                Button goButton = gameOverBtn.GetComponent<Button>();
                goButton.onClick.Invoke();
                break;
        }
      
    }
}
