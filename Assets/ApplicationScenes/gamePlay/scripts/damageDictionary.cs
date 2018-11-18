using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDictionary : MonoBehaviour {

    /// <summary>
    /// (Sprite's tag, damage)
    /// </summary>
    public Dictionary<string, double> damages = new Dictionary<string, double>();

	// Use this for initialization
	public void Start () {
        damages.Add("BaseBot", 20);
        damages.Add("Drone", 30);
        damages.Add("Player", 20);
        damages.Add("Spawner", 40);
        damages.Add("BaseLaser", 10);
	}
}
