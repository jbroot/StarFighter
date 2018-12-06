using System.Collections.Generic;

public class damageDictionary{

    /// <summary>
    /// (Sprite's tag, damage)
    /// </summary>
    public Dictionary<string, float> damages = new Dictionary<string, float>();

	// Use this for initialization
	public damageDictionary() {
        damages.Add("BaseBot", 30);
        damages.Add("Drone", 70);
        damages.Add("Player", 30);
        damages.Add("BaseLaser", 10);
        damages.Add("Asteroid", 40);
        damages.Add("Untagged", 40);
	}
}
