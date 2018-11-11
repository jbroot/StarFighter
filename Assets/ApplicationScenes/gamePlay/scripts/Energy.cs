using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    public float CurrentEnergy = 0f;
    public float MaxEnergy = 100f;
    public float RegenerateRate = 1f;

    public Slider EnergyBar;

    // Use this for initialization
	void Start ()
	{
	    CurrentEnergy = MaxEnergy;
        InvokeRepeating("RegenerateEnergy", 0.001f, 0.02f);
	}

    void RegenerateEnergy()
    {
        if (CurrentEnergy >= MaxEnergy)
        {
            return;
        }

        CurrentEnergy += RegenerateRate;
    }

    private void UpdateEnergyBar(Slider energyBar)
    {
        energyBar.value = CurrentEnergy / MaxEnergy;
    }

    // Update is called once per frame
    void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.LeftControl))
	    {
	        if (CurrentEnergy >= 25)
	        {
	            CurrentEnergy -= 25;
	        }
	    }

	    UpdateEnergyBar(EnergyBar);
    }
}
