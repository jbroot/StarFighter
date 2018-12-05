using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not currently used
/// </summary>
public class botList {
    /// <summary>
    /// List of alive drones
    /// </summary>
    public List<GameObject> droneList;
    /// <summary>
    /// List of alive baseBots
    /// </summary>
    public List<GameObject> baseBotList;


    /// <summary>
    /// Adds a bot to a list
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="gameObject"></param>
    public void addBot(string tag, GameObject gameObject)
    {
        if (gameObject == null) Debug.Log("null");
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
