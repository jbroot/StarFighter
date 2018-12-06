using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Spawner : MonoBehaviour {

    public GameObject[] asteroidPrefabArray;
    

    System.Random rnd = new System.Random();

    public float asteroidRate = 999f;
    public float startingAsteroidCount = 20;
    float nextAsteroid = 1f;
    float spawnDistance = 1f;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < startingAsteroidCount; i++)
        {

            int asteroidPick = rnd.Next(0, asteroidPrefabArray.Length-1);

            spawnDistance = Random.value * 100;

            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance;
            

            //print("Spawning Asteroid");
            GameObject Asteroid = Instantiate(asteroidPrefabArray[asteroidPick], transform.position + offset,Quaternion.identity);
            Asteroid.tag = "Asteroid";

        }
    }

    // Update is called once per frame
    void Update()
    {
        nextAsteroid -= Time.deltaTime;

        if (nextAsteroid <= 0)
        {
            nextAsteroid = asteroidRate;

            spawnDistance = Random.value * 25;

            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance;

            //print("Spawning Asteroid");
            //Instantiate(asteroidPrefab, transform.position + offset, Quaternion.identity);
        }

    }
}
