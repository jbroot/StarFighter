﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDictionary{

    /// <summary>
    /// (Sprite's tag, point value)
    /// </summary>
    public Dictionary<string, double> scores = new Dictionary<string, double>();

    // Use this for initialization
    public ScoreDictionary()
    {
        scores.Add("BaseBot", 1);
        scores.Add("Drone", 5);
        scores.Add("Player", 50);
        scores.Add("Asteroid", 20);
    }
}
