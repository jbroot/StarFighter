using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public GameObject bulletPrefab;

	public float timer = 2f;
	public float time;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Destroy(bulletPrefab);
		}
	}
}
