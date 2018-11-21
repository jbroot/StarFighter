using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

	public GameObject bulletPrefab;
	public float fireDelay = 0.25f;
	float cooldownTimer = 0;

	public GameObject player;
	//public debug;
	// Use this for initialization
	void Start()
	{

		//player = GameObject.FindWithTag("Player");

	}

	void Update()
	{
		cooldownTimer -= Time.deltaTime;

		if (Input.GetButton("Fire1") && cooldownTimer <= 0)
		{
			//debug.log("Pew!");
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * new Vector3(0, 0.5f, 0);

			Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
		}
	}
}
