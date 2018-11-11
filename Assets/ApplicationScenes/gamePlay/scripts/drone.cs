using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : baseBot {

    protected override Quaternion rotate()
    {
        // Get the Quaternion
        Quaternion rot = transform.rotation;

        float degreeToTarget = findDegree(transform.position - target.transform.position);

        transform.rotation = Quaternion.Euler(0, 0, degreeToTarget);

        return transform.rotation;

        /*// Move the ship
        Vector3 pos = transform.position;

        Vector3 vel = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);

        pos += rot * vel;

        transform.position = pos;
        /*
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, 
            rotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDir);*/
    }
}
