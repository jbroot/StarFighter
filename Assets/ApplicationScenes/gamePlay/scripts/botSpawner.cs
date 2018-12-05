using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botSpawner : MonoBehaviour {

    public GameObject baseBotObject;
    public GameObject droneObject;

    public int numBaseBotsMax = 10;
    public int numDronesMax = 5;

    public float secondsBetweenBaseBots = 5;
    public float secondsBetweenDrones = 5;

    float baseBotTimer = 1;
    float droneTimer = 1;

	// Use this for initialization
	void Start () {
        GameObject drone = makeBot(ref droneObject);
        GameObject baseBot = makeBot(ref baseBotObject);

        botList.droneList.Add(drone);
        botList.droneList.Add(baseBot);
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
        if (baseBotTimer <= 0 && botList.baseBotList.Count < numBaseBotsMax)
        {
            GameObject bot = makeBot(ref baseBotObject);
            baseBotTimer = secondsBetweenBaseBots;
            botList.addBot(bot.tag, bot);
        }
        if(droneTimer <= 0 && botList.droneList.Count < numDronesMax)
        {
            GameObject bot = makeBot(ref droneObject);
            droneTimer = secondsBetweenDrones;
            botList.addBot(bot.tag, bot);
        }

        baseBotTimer -= Time.deltaTime;
        droneTimer -= Time.deltaTime;
    }

    GameObject makeBot(ref GameObject sprite)
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-25, 25f), 0);
        Quaternion rot = new Quaternion(0, 0, 0, 1);

        return Instantiate(sprite, pos, rot);
    }
}
