using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using System.Threading;

public class Health : MonoBehaviour
{
    public AudioClip boomSound;
    public Sprite boom;
    public float CurrentHealth = 0f;
    public float MaxHealth = 100f;

    public Slider HealthBar;

    // Use this for initialization
    void Start()
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
        //Colliding with bots
        if (collidingObjectName.Contains("bot"))
        {
            CurrentHealth -= 25;
            return;
        }

        //Colliding with Asteroids
        if (collidingObjectName.Contains("Asteroid"))
        {
            CurrentHealth -= 50;
            return;
        }

        //Default if colliding with something not in the list.
        CurrentHealth -= 20;
    }

    void UpdateHealthBar(Slider healthBar)
    {
        healthBar.value = CurrentHealth / MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
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
        GetComponent<SpriteRenderer>().sprite = boom;
        /*AudioSource music = GetComponent<AudioSource>();
        music.clip = boomSound;
        music.Play();*/
        //menu
        //Destroy(ship);
    }
}