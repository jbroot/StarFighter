using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class damageDictionary : MonoBehaviour {

    /// <summary>
    /// (Sprite's tag, damage)
    /// </summary>
    public static Dictionary<string, double> damages = new Dictionary<string, double>();

	// Use this for initialization
	void Start () {
        damages.Add("BaseBot", 20);
        damages.Add("Drone", 30);
        damages.Add("Player", 20);
        damages.Add("Spawner", 40);
        damages.Add("BaseLaser", 10);
	}
}
