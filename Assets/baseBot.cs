using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class baseBot : MonoBehaviour
{

    public float height = 1;
    public float width = 1;
    /// <summary>
    /// initial position
    /// </summary>
    public Vector2 initialPosition = new Vector2(1, 0);
    /// <summary>
    /// list of players to search
    /// </summary>
    public List<SpriteRenderer> players;
    /// <summary>
    /// list of bots to consider
    /// </summary>
    public List<SpriteRenderer> otherBots;
    /// <summary>
    /// how close to other bots is too close
    /// </summary>
    float personalBotSpace = 2;
    /// <summary>
    /// Maximum radius of players to consider. May be affected by bias/score
    /// </summary>
    public float radar = 30;
    public int maxXPosition = 5000;
    public int minXPosition = -5000;
    public int maxYPosition = 5000;
    public int minYPosition = -5000;
    /// <summary>
    /// max velocity for this bot
    /// </summary>
    public float maxSpeed = 4.5f;
    /// <summary>
    /// What score is considered to be 100% in the score-bias search alternative
    /// </summary>
    public int maxScore = 100000;
    /// <summary>
    /// speed allowed for rotation
    /// </summary>
    public float rotationSpeed = 180f;

    /// <summary>
    /// sprite to target
    /// </summary>
    private SpriteRenderer target;
    /// <summary>
    /// score bias
    /// </summary>
    private float maxBias = 0;
    /// <summary>
    /// [maxX, minX, maxY, minY] if no target is found then avoid this zone
    /// </summary>
    private int[] softBounds;

    /// <summary>
    /// initialization
    /// </summary>
    void Start()
    {
        //scale the sprite
        transform.localScale = new Vector2(width, height);
        //spawn
        transform.position = initialPosition;
        //rotate to 0 degrees
        Quaternion rotation = transform.localRotation;
        rotation.z = 0;

        //initialize softBounds
        int boundDifference = 100;
        softBounds = new int[] {maxXPosition - boundDifference, minXPosition - boundDifference,
            maxYPosition - boundDifference, minYPosition - boundDifference};
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!findTarget())
        {
            //no target found
            return;
        }
        //movement
        changeVelocity();

        //rotate
        rotate();

        //TODO: predict enemy position over time
    }

    /// <summary>
    /// Finds and changes the velocity for this bot
    /// </summary>
    /// <returns></returns>
    void changeVelocity()
    {
        //slows down to reduce collision chance
        Vector2 dif = target.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position;
        float newVelocity = getMagnitude(dif);
        if (newVelocity > maxSpeed) newVelocity = maxSpeed;

        float frameMovement = newVelocity * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, frameMovement);

        //consider other bots' positions
        asocialBots();
    }

    /// <summary>
    /// avoids being in the proximity of other bots
    /// </summary>
    void asocialBots()
    {
        foreach (SpriteRenderer bot in otherBots)
        {
            if (getMagnitude(bot.transform.position - transform.position) <= personalBotSpace)
            {
                transform.position = Vector2.MoveTowards(transform.position, bot.transform.position, -Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Finds target based on proximity. Returns false if no target is found
    /// </summary>
    /// <returns></returns>
    bool findTarget()
    {
        //maybe: sort players into grids

        //get enemies positions, velocity, etc
        //find closest enemy
        target = null;
        float tempRadar = radar;
        foreach (SpriteRenderer player in players)
        {
            //skip player if outside bots' boundaries
            if(player.transform.position[0] > maxXPosition || player.transform.position[0] < minXPosition ||
                player.transform.position[1] > maxYPosition || player.transform.position[1] < minYPosition)
            {
                continue;
            }

            //TODO: find player's score
            float score = 0;

            //circle view
            Vector2 dif = transform.position - player.transform.position;
            float distance = getMagnitude(dif);
            //minus (percentOfMaxScore*2)^3 to bias targeting towards those that have more points. 
            float bias = Mathf.Pow(score / maxScore * 50, 3);
            if(bias > maxBias)
            {
                bias = maxBias;
            }
            distance -= bias;
            if (distance > tempRadar) continue;
            else
            {
                target = player;
                tempRadar = distance;
            }
        }

        //if no players are in range
        if (target == null)
        {
            //TODO: have bots go elsewhere
            if (transform.position[0] > softBounds[0])
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else if(transform.position[0] < softBounds[1])
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            if(transform.position[1] > softBounds[2])
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else if (transform.position[1] < softBounds[3])
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            //gradually slow down
            GetComponent<Rigidbody2D>().velocity = scalarTimesVector(0.9f, GetComponent<Rigidbody2D>().velocity);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Finds the degree for Vector1 to point to vector2 in vector1-vector2
    /// </summary>
    /// <param name="dif"></param>
    /// <returns>float</returns>
    float findDegree(Vector2 dif)
    {
        //Mathf.Atan2(dif.y, dif.x);
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
            float degree = Mathf.Atan(dif[1] / dif[0]) * Mathf.Rad2Deg;
            if (dif[0] < 0) return degree - 90;
            else return degree + 90;
        }
    }

    /// <summary>
    /// Returns magnitude from a 2d vector
    /// x^2+y^2 = (return value)^2
    /// </summary>
    /// <param name="dif"></param>
    /// <returns></returns>
    float getMagnitude(Vector2 dif)
    {
        return Mathf.Pow(dif[0] * dif[0] + dif[1] * dif[1], 0.5f);
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

    /// <summary>
    /// rotates bot
    /// </summary>
    void rotate()
    {
        //find desired angle
        float degreeToTarget = findDegree(transform.position - target.transform.position);

        transform.rotation = Quaternion.Euler(0, 0, degreeToTarget);

        /*
        float z = transform.rotation.eulerAngles.z;

        float rotationDist = z - degreeToTarget;

        if((rotationDist > 0 && rotationDist < 180) || rotationDist < -180)
        {
            z = z - rotationSpeed * Time.deltaTime;
        }
        else if(rotationDist != 0)
        {
            z = z + rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, 0, z);
        return;*/
    }

}
