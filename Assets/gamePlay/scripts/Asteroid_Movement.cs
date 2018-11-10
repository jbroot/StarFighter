using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Movement : MonoBehaviour {

    //float torqueAmount = Random.Range(0,50);
    public Rigidbody2D aster;

    // Use this for initialization
    void Start()
    {
        float torqueAmount = Random.Range(-50, 50);
        aster = GetComponent<Rigidbody2D>();
        aster.AddTorque(torqueAmount, ForceMode2D.Force);

    }


	

}
