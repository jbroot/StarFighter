using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    private Rigidbody2D rb;
    public float maxVelocity = 5;
    public float rotationSpeed = 3;

    #region MonoBehaviour API
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    private void FixedUpdate()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(yAxis);
        Rotate(transform, xAxis * -rotationSpeed * Time.deltaTime);
    }


    #region Maneuvering API
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount * 5;

        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }


    #endregion


    #region Old Movement Code
    //   public float maxSpeed = 5f;
    //   public float rotationSpeed = 180f;

    //// Update is called once per frame
    //void Update () {
    //       // Get the Quaternion
    //       Quaternion rot = transform.rotation;

    //       //Get Z Euler Angles
    //       float z = rot.eulerAngles.z;

    //       //Change Z angle based on input
    //       z -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

    //       //Recreate the Quaternion
    //       rot = Quaternion.Euler(0, 0, z);

    //       //Feed Quaternion into our rotation
    //       transform.rotation = rot;

    //       //Move the ship
    //       Vector3 pos = transform.position;

    //       Vector3 vel = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime,0);


    //       pos += rot * vel;

    //       transform.position = pos;

    //}
    #endregion
}
