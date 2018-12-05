using UnityEngine;

public class DamageHandler : MonoBehaviour {

	public int health = 1;

	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;

	void start()
	{
		correctLayer = gameObject.layer;
	}

	void onTriggerEnter2D()
	{
		Debug.Log("Trigger!");

		health--;
		invulnTimer = invulnPeriod;
		gameObject.layer = 10;
	}

	void Update()
	{
		invulnTimer -= Time.deltaTime;
		if (invulnTimer <= 0)
		{
			gameObject.layer = correctLayer;
		}


	}
}
