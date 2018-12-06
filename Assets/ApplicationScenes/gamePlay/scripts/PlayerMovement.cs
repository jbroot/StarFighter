using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private Rigidbody2D rb;
    public float maxVelocity = 5;
    public float rotationSpeed = 3;

    public float halfWidth = 50;
    public float halfHeight = 50;

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

        Rotate(xAxis * -rotationSpeed * Time.deltaTime);

        boundaries();

        ThrustForward(yAxis, gameObject);
    }


    #region Maneuvering API
    /// <summary>
    /// Enforce boundaries
    /// </summary>
    private void boundaries()
    {
        Vector2 tempPos = transform.position;
        //Can't go past boundaries
        if (transform.position.x < -halfWidth)
        {
            tempPos.x = -halfWidth;
        }
        else if (transform.position.x > halfWidth)
        {
            tempPos.x = halfWidth;
        }
        else if (transform.position.y < -halfHeight)
        {
            tempPos.y = -halfHeight;
        }
        else if (transform.position.y > halfHeight)
        {
            tempPos.y = halfHeight;
        }
        else return;

        transform.position = tempPos;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    /*private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }*/

    private void ThrustForward(float amount, GameObject gmObject)
    {
        Vector2 force = transform.up * amount * 5;

        rb.AddForce(force);

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    private void Rotate(float amount)
    {
        transform.Rotate(0, 0, amount);
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
