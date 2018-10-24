using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed = 5f;
    public float rotationSpeed = 180f;
	
	// Update is called once per frame
	void Update () {
        // Get the Quaternion
        Quaternion rot = transform.rotation;

        //Get Z Euler Angles
        float z = rot.eulerAngles.z;

        //Change Z angle based on input
        z -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        //Recreate the Quaternion
        rot = Quaternion.Euler(0, 0, z);

        //Feed Quaternion into our rotation
        transform.rotation = rot;

        //Move the shio
        Vector3 pos = transform.position;

        Vector3 vel = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime,0);


        pos += rot * vel;

        transform.position = pos;






	}
}
