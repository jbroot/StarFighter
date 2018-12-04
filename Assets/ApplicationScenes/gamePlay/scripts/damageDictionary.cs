using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDictionary{

    /// <summary>
    /// (Sprite's tag, damage)
    /// </summary>
    public Dictionary<string, float> damages = new Dictionary<string, float>();

	// Use this for initialization
	public damageDictionary() {
        damages.Add("BaseBot", 20);
        damages.Add("Drone", 30);
        damages.Add("Player", 20);
        damages.Add("BaseLaser", 10);
        damages.Add("Asteroid", 50);
        damages.Add("Untagged", 50);
	}
}
