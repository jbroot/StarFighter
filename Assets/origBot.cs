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

    // Use this for initialization
    void Start()
    {
        //scale the sprite
        transform.localScale = new Vector2(width, height);
        //spawn
        transform.position = position;
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

        SpriteRenderer target = null;
        float tempRadar = radar;
        foreach (SpriteRenderer player in players)
        {
            //TODO: find player's score
            float score = 0;

            //circle view
            Vector2 dif = transform.position - player.transform.position;
            float magnitude = getMagnitude(dif);
            //minus (percentOfMaxScore*2)^3 to bias targeting towards those that have more points. 
            //max bias of magnitude -9
            magnitude -= Mathf.Pow((score / maxScore) * 50, 3);
            if (magnitude > tempRadar) continue;
            else
            {
                target = player;
                tempRadar = magnitude;
            }
        }
        //if no players are in range
        if (target == null)
        {
            //gradually slow down
            rigidbody2D.velocity = scalarTimesVector(0.9f, rigidbody2D.velocity);
            return;
        }

        //TODO: find angle

        //slows down to reduce collision chance
        Vector2 newVelocity = target.GetComponent<Rigidbody2D>().position - rigidbody2D.position;
        //adds some repulsion
        newVelocity[0] -= repulsion;
        newVelocity[1] -= repulsion;
        //proportional to max speed if magnitude is greater
        if (getMagnitude(newVelocity) > maxSpeed)
        {
            float absVelX = Mathf.Abs(newVelocity[0]);
            float absVelY = Mathf.Abs(newVelocity[1]);
            float sum = absVelX + absVelY;
            newVelocity = new Vector2((absVelX / sum) * maxSpeed, (absVelY / sum) * maxSpeed);
        }

        //move bot
        rigidbody2D.velocity = newVelocity;

        //predict enemy position over time

        //if no enemies in range then choose a direction
        //if edge of map change direction

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

}
