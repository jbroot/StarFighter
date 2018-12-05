using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class botList : MonoBehaviour {
    /// <summary>
    /// List of alive drones
    /// </summary>
    public List<GameObject> droneList;
    /// <summary>
    /// List of alive baseBots
    /// </summary>
    public List<GameObject> baseBotList;

	/// <summary>
    /// Initialize lists
    /// </summary>
	void Start () {
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        GameObject[] baseBots = GameObject.FindGameObjectsWithTag("BaseBot");

        foreach (GameObject bot in drones)
        {
            droneList.Add(bot);
        }
        foreach (GameObject bot in baseBots)
        {
            baseBotList.Add(bot);
        }
    }


    /// <summary>
    /// Adds a bot to a list
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="gameObject"></param>
    public void addBot(string tag, GameObject gameObject)
    {
        if (tag == "Drone")
        {
            droneList.Add(gameObject);
        }
        else if (tag == "BaseBot")
        {
            baseBotList.Add(gameObject);
        }
        else
        {
            throw new Exception("Lists with that tag are not supported");
        }
    }

    /// <summary>
    /// Removes a bot from a list
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="gameObject"></param>
    public void removeBot(string tag, GameObject gameObject)
    {
        if (tag == "Drone")
        {
            foreach (GameObject bot in droneList)
            {
                if(bot == gameObject)
                {
                    droneList.Remove(bot);
                }
            }
        }
        else if (tag == "BaseBot")
        {

            foreach (GameObject bot in baseBotList)
            {
                if (bot == gameObject)
                {
                    baseBotList.Remove(bot);
                }
            }
        }
        else
        {
            throw new Exception("Lists with that tag are not supported.");
        }
    }
}
