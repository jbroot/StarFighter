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
    public Collision2D MostRecentCollision;
    public damageDictionary DmgDictionary;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
        if (HealthBar != null)
        {
            UpdateHealthBar(HealthBar);
        }
    }

    public void Respawn()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar(HealthBar);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        MostRecentCollision = collision;
        TakeDamage(collision.gameObject.tag);
        if (HealthBar != null)
        {
            UpdateHealthBar(HealthBar);
        }
    }

    void TakeDamage(string collidingObjectTag)
    {
        CurrentHealth -= (int) DmgDictionary.damages[collidingObjectTag];
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
                gameObject.GetComponent<Lives>().LoseALife(MostRecentCollision);
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