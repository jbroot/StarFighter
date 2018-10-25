using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class origBot : MonoBehaviour
{

    public float height = 2;
    public float width = 1;
    public Vector2 position = new Vector2(1, 0);
    public List<SpriteRenderer> players;
    public float radar = 30;
    public int maxXPosition = 5000;
    public int minXPosition = -5000;
    public int maxYPosition = 5000;
    public int minYPosition = -5000;
    public float maxSpeed = 5;
    public int maxScore = 100000;
    float repulsion = 0.75f;
    public float rotationSpeed = 180f;

    private SpriteRenderer target;

    // Use this for initialization
    void Start()
    {
        //scale the sprite
        transform.localScale = new Vector2(width, height);
        //spawn
        transform.position = position;
        //rotate to 0 degrees
        Quaternion rotation = transform.localRotation;
        rotation.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();
        //get current position, velocity, etc
        //rigidbody2D.velocity = new Vector2(2, 3);


        //get enemies positions, velocity, etc
        //find closest enemy
        //player.X is player, other is bot

        //maybe sort players into grids

        target = null;
        float tempRadar = radar;
        foreach (SpriteRenderer player in players)
        {
            //TODO: find player's score
            float score = 0;

            //circle view
            Vector2 dif = transform.position - player.transform.position;
            float magnitude1 = getMagnitude(dif);
            //minus (percentOfMaxScore*2)^3 to bias targeting towards those that have more points. 
            //max bias of magnitude -9
            magnitude1 -= Mathf.Pow((score / maxScore) * 50, 3);
            if (magnitude1 > tempRadar) continue;
            else
            {
                target = player;
                tempRadar = magnitude1;
            }
        }
        //if no players are in range
        if (target == null)
        {
            //gradually slow down
            rigidbody2D.velocity = scalarTimesVector(0.9f, rigidbody2D.velocity);
            return;
        }
        
        //slows down to reduce collision chance
        Vector2 newVelocity = target.GetComponent<Rigidbody2D>().position - rigidbody2D.position;
        //adds some repulsion
        newVelocity[0] -= repulsion;
        newVelocity[1] -= repulsion;
        //find desired angle
        float degree = findDegree(transform.position - target.transform.position);
        Debug.Log(transform.position - target.transform.position);
        Debug.Log("desired degree = " + degree.ToString());

        float magnitude = getMagnitude(newVelocity);

        //rotate and move bot
        if (magnitude >= maxSpeed)
        {
            rotate(degree, maxSpeed);
        }
        else
        {
            rotate(degree, magnitude);
        }

        //move bot
        //rigidbody2D.velocity = newVelocity * Time.deltaTime;

        //predict enemy position over time

        //if no enemies in range then choose a direction
        //if edge of map change direction

    }

    /// <summary>
    /// Finds the degree for Vector1 to point to vector2 in vector1-vector2
    /// </summary>
    /// <param name="dif"></param>
    /// <returns>float</returns>
    float findDegree(Vector2 dif)
    {
        Debug.Log("xDif: " + dif[0].ToString() + "yDif: " + dif[1].ToString());
        //avoid division by 0
        if (dif[0] == 0)
        {
            if (dif[1] > 0) return 0;
            else return 180;
        }
        //pretend origin is the bot
        //arctan is always y/x in this case
        else
        {
            return -Mathf.Atan(dif[1] / dif[0]) * 180 / Mathf.PI;
        }
    }

    /// <summary>
    /// Returns magnitude from a 2d vector
    /// x^2+y^2 = (return value)^2
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    float getMagnitude(Vector2 dif)
    {
        return Mathf.Pow(dif[0] * dif[0] + dif[1] * dif[1], (float)0.5);
    }

    /// <summary>
    /// multiplies x and y vectors by a scalar
    /// Syntax: SCALAR * 2DVECTOR = 2DVECTOR
    /// </summary>
    /// <param name="origVector"></param>
    /// <returns>Vector2</returns>
    Vector2 scalarTimesVector(float scalar, Vector2 origVector)
    {
        origVector[0] *= scalar;
        origVector[1] *= scalar;
        return origVector;
    }

    void rotate(float degreeToTarget, float velocity)
    {
        if (degreeToTarget < 0) degreeToTarget += 360;
        // Get the Quaternion
        Quaternion rot = transform.rotation;
        //Get Z Euler Angles
        float z = rot.eulerAngles.z;
        Debug.Log("current d=" + z.ToString());

        //Change Z angle based on target's position
        //TODO: keep rotation speed
        float absDif = Mathf.Abs(degreeToTarget - z);
        if (absDif >= 180)
        {
            if (degreeToTarget < z)
            {
                z += rotationSpeed * Time.deltaTime;
            }
            else
            {
                z -= rotationSpeed * Time.deltaTime;
            }
        }
        else if (absDif < 180 && absDif > 0)
        {
            if (degreeToTarget > z)
            {
                z += rotationSpeed * Time.deltaTime;
            }
            else
            {
                z -= rotationSpeed * Time.deltaTime;
            }
        }
        z = degreeToTarget;
        Debug.Log("new d=" + z.ToString());
        rot = Quaternion.Euler(0, 0, -z);

        transform.rotation = rot;

        //proportinally allot velocity to x and y velocity vectors
        GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity * Mathf.Sin(z), -velocity * Mathf.Cos(z));
        Debug.Log("velocity= " + GetComponent<Rigidbody2D>().velocity);
    }

}
