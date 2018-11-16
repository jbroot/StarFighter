using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDictionary : MonoBehaviour {

    public Dictionary<string, double> damages = new Dictionary<string, double>();

	// Use this for initialization
	void Start () {
        damages.Add("baseBotContact", 20);
        damages.Add("droneContact", 30);
        damages.Add("playerContact", 20);
        damages.Add("spawnerContact", 40);
        damages.Add("baseLaser", 10);
	}
}
