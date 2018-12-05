using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

	public GameObject bulletPrefab;
	public float fireDelay = 0.25f;
	float cooldownTimer = 0;

    public AudioClip shootSound;
    private AudioSource source { get { return GetComponent<AudioSource>(); }}
    public GameObject player;
	//public debug;
	// Use this for initialization
	void Start()
	{
        gameObject.AddComponent<AudioSource>();
        source.clip = shootSound;
        source.playOnAwake = true;
    }

    void playShootingSound(){
        source.PlayOneShot(shootSound);
    }
	void Update()
	{
		cooldownTimer -= Time.deltaTime;
      
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
		{
            playShootingSound();
            cooldownTimer = fireDelay;
			Vector3 offset = transform.rotation * new Vector3(0, 0.75f, 0);
            bulletPrefab.tag = "BaseLaser";
			var laser = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            //Physics2D.IgnoreCollision(laser.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
	}
}
