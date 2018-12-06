using System;
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
    public damageDictionary DamageDictionary;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar(HealthBar);
        DamageDictionary = new damageDictionary();
    }

    public void Respawn()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar(HealthBar);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision.gameObject.tag);
        UpdateHealthBar(HealthBar);
    }

    void TakeDamage(string collidingObjectTag)
    {
        CurrentHealth -= (int) DamageDictionary.damages[collidingObjectTag];
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
    IEnumerable Boom(GameObject ship)
    {
        GetComponent<SpriteRenderer>().sprite = boom;
        yield return new WaitForSeconds(1.5f);
        Destroy(ship);
        /*AudioSource music = GetComponent<AudioSource>();
        music.clip = boomSound;
        music.Play();*/
        //menu
        //Destroy(ship);
    }
}