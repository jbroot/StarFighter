using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botSpawner : MonoBehaviour {

    public GameObject baseBotObject;
    public GameObject droneObject;

    public int numBaseBotsMax = 10;
    public int numDronesMax = 5;

    public int boundaryWidth = 25;
    public int boundaryHeight = 25;

    public float secondsBetweenBaseBots = 5;
    public float secondsBetweenDrones = 5;

    //public botList botLists;

    float baseBotTimer = 0;
    float droneTimer = 0;

	// Use this for initialization
	void Start () {
        /*
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        GameObject[] baseBots = GameObject.FindGameObjectsWithTag("BaseBot");

        foreach (GameObject bot in drones)
        {
            botList.droneList.Add(bot);
        }
        foreach (GameObject bot in baseBots)
        {
            botList.baseBotList.Add(bot);
        }*/
    }
	
	// Update is called once per frame
	void Update () {

        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        GameObject[] baseBots = GameObject.FindGameObjectsWithTag("BaseBot");

        if (baseBotTimer <= 0 && baseBots.Length < numBaseBotsMax)
        {
            GameObject bot = makeBot(ref baseBotObject);
            baseBotTimer = secondsBetweenBaseBots;
        }
        if(droneTimer <= 0 && drones.Length < numDronesMax)
        {
            GameObject bot = makeBot(ref droneObject);
            droneTimer = secondsBetweenDrones;
        }

        baseBotTimer -= Time.deltaTime;
        droneTimer -= Time.deltaTime;
    }

    GameObject makeBot(ref GameObject sprite)
    {
        Vector3 pos = new Vector3(Random.Range(-boundaryWidth, boundaryWidth), Random.Range(-boundaryHeight, boundaryHeight), 0);
        Quaternion rot = new Quaternion(0, 0, 0, 1);

        return Instantiate(sprite, pos, rot);
    }
}
