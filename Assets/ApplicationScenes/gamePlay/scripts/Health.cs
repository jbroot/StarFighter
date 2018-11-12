using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float CurrentHealth = 0f;
    public float MaxHealth =100f;

    public Slider HealthBar;

	// Use this for initialization
	void Start ()
	{
	    CurrentHealth = MaxHealth;
        UpdateHealthBar(HealthBar);
	}

    public void Respawn()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar(HealthBar);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision.gameObject.name);
        UpdateHealthBar(HealthBar);
    }

    void TakeDamage(string collidingObjectName)
    {
        if (collidingObjectName == "bot" || collidingObjectName == "bot (1)")
        {
            CurrentHealth -= 25;
        }
    }

    void UpdateHealthBar(Slider healthBar)
    {
        healthBar.value = CurrentHealth / MaxHealth;
    }

	// Update is called once per frame
	void Update () {
	    if (CurrentHealth <= 0)
	    {
	        if (gameObject.GetComponent<Lives>() != null)
	        {
	            gameObject.GetComponent<Lives>().LoseALife();
	            return;
	        }
	        Boom(gameObject);
	    }
	}
    void Boom(GameObject ship)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load("Assets/Universal/boom1") as Sprite;
        //Destroy(ship);
    }
}
