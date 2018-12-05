using System.Collections.Generic;
using UnityEngine;

public class baseBot : MonoBehaviour
{
    #region public attributes
    /// <summary>
    /// The laser sound
    /// </summary>
    public AudioClip shootSound;
    /// <summary>
    /// The sprite for death animation
    /// </summary>
    public Sprite boom;
    /// <summary>
    /// The bot's health
    /// </summary>
    public float health = 100;
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
    public float personalBotSpace = 2;
    /// <summary>
    /// Maximum radius of players to consider. May be affected by bias/score
    /// </summary>
    public float radar = 30;
    /// <summary>
    /// Laser that is shot
    /// </summary>
    public GameObject bulletPrefab;
    public int maxXPosition = 500;
    public int minXPosition = -500;
    public int maxYPosition = 500;
    public int minYPosition = -500;
    /// <summary>
    /// max velocity for this bot
    /// </summary>
    public float maxVelocity = 4.5f;
    /// <summary>
    /// What score is considered to be 100% in the score-bias search alternative
    /// </summary>
    public int maxScore = 100000;
    /// <summary>
    /// speed allowed for rotation
    /// </summary>
    public float rotationSpeed = 180f;
    /// <summary>
    /// How long to delay between shots
    /// </summary>
    public float delayFireSec = 0.5f;
    /// <summary>
    /// How long to show the explosion
    /// </summary>
    public float secondsBoomLasts = 1.3f;
    #endregion

    #region protected attributes
    /// <summary>
    /// Has object exploded
    /// </summary>
    protected bool isBoom = false;
    /// <summary>
    /// flag for backing up
    /// </summary>
    protected bool isBackingUp;
    /// <summary>
    /// The damage dictionary
    /// </summary>
    protected damageDictionary damageDictionary;
    /// <summary>
    /// sprite to target
    /// </summary>
    protected SpriteRenderer target;
    /// <summary>
    /// score bias
    /// </summary>
    protected float maxBias = 0;
    /// <summary>
    /// [maxX, minX, maxY, minY] if no target is found then avoid this zone
    /// </summary>
    protected int[] softBounds;
    /// <summary>
    /// Fire ready when cooldownTimer == 0
    /// </summary>
    protected float cooldownTimer = 0;
    /// <summary>
    /// Desired degree
    /// </summary>
    protected float degreeToTarget = 0;
    /// <summary>
    /// Rigidbody2D most recently collided with
    /// </summary>
    protected Rigidbody2D recentCollision;
    #endregion

    /// <summary>
    /// initialization
    /// </summary>
    protected virtual void Start()
    {
        damageDictionary = new damageDictionary();
        
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
    protected virtual void Update()
    {
        if (isBoom)
        {
            secondsBoomLasts -= Time.deltaTime;
            if(secondsBoomLasts < 0)
            {
                GetComponent<SpriteRenderer>().sprite = null;
                Destroy(gameObject);
            }
            return;
        }
        if (!findTarget())
        {
            //no target found
            return;
        }

        //movement and rotation
        changeVelocity();

        shouldShoot();

        //TODO: predict enemy position over time
    }

    /// <summary>
    /// Finds target based on proximity. Returns false if no target is found
    /// </summary>
    /// <returns></returns>
    protected virtual bool findTarget()
    {
        //maybe: sort players into grids

        //get enemies positions, velocity, etc
        //find closest enemy
        target = null;
        float tempRadar = radar;
        foreach (SpriteRenderer player in players)
        {
            //skip player if outside bots' boundaries
            if(player != null){
                if (player.transform.position[0] > maxXPosition || player.transform.position[0] < minXPosition ||
                    player.transform.position[1] > maxYPosition || player.transform.position[1] < minYPosition)
                {
                    continue;
                }
            }else{
                continue;
            }
           

            //TODO: find player's score
            float score = 0;

            //circle view
            Vector2 dif = transform.position - player.transform.position;
            float distance = getMagnitude(dif);
            //minus (percentOfMaxScore*2)^3 to bias targeting towards those that have more points. 
            float bias = Mathf.Pow(score / maxScore * 50, 3);
            if (bias > maxBias)
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
            else if (transform.position[0] < softBounds[1])
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            if (transform.position[1] > softBounds[2])
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

    #region Movement

    /// <summary>
    /// Finds and changes the velocity for this bot
    /// </summary>
    /// <param name="timesWithProjectedVelocity">Used to back up usually</param>
    protected virtual void changeVelocity()
    {
        //prioritize not colliding with other bots
        if (asocialBots()) return;

        Quaternion rot = rotate();
        //slows down to reduce collision chance
        Vector2 dif = target.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position;
        float newVelocity = getMagnitude(dif);
        if (newVelocity > maxVelocity) newVelocity = maxVelocity;
        /*
        if (isBackingUp)
        {
            newVelocity = -newVelocity;
            //if far enough away from collision then reset isBackingUp
            isBackingUp = getMagnitude(transform.GetComponent<Rigidbody2D>().position - recentCollision.position) > 2;
        }*/
        Vector3 frameMovement = new Vector3(0, newVelocity * Time.deltaTime, 0);
        transform.position += rot * frameMovement;

    }

    /// <summary>
    /// avoids being in the proximity of other bots
    /// </summary>
    protected virtual bool asocialBots()
    {
        foreach (SpriteRenderer bot in otherBots)
        {
            if (getMagnitude(bot.transform.position - transform.position) <= personalBotSpace)
            {
                //moves away from that bot
                transform.position = Vector2.MoveTowards(transform.position, bot.transform.position, -Time.deltaTime * maxVelocity);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// rotates bot
    /// </summary>
    protected virtual Quaternion rotate()
    {

        // Get the Quaternion
        Quaternion rot = transform.rotation;

        //Get a Euler angle
        float z = rot.eulerAngles.z % 360;

        degreeToTarget = findDegree(transform.position - target.transform.position);
        float rotationDist = z - degreeToTarget;

        if ((rotationDist > 0 && rotationDist < 180) || rotationDist < -180)
        {
            z = z - rotationSpeed * Time.deltaTime;
        }
        else if (rotationDist != 0)
        {
            z = z + rotationSpeed * Time.deltaTime;
        }

        //Recreate the Quaternion
        rot = Quaternion.Euler(0, 0, z);

        //Feed Quaternion into our rotation
        transform.rotation = rot;

        return rot;
    }

    #endregion

    #region Math Help Functions
    /// <summary>
    /// Finds the degree for Vector1 to point to vector2 in vector1(Self)-vector2(target)
    /// Degree returned is [0,360)
    /// 
    /// </summary>
    /// <param name="dif"></param>
    /// <returns>float</returns>
    protected virtual float findDegree(Vector2 dif)
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
            if (dif[0] < 0) degree -= 90;
            else degree += 90;
            while (degree < 0)
            {
                degree += 360;
            }
            return degree % 360;
        }
    }

    /// <summary>
    /// Returns magnitude from a 2d vector
    /// x^2+y^2 = (return value)^2
    /// </summary>
    /// <param name="dif"></param>
    /// <returns></returns>
    protected virtual float getMagnitude(Vector2 dif)
    {
        return Mathf.Pow(dif[0] * dif[0] + dif[1] * dif[1], 0.5f);
    }

    /// <summary>
    /// multiplies x and y vectors by a scalar
    /// Syntax: SCALAR * 2DVECTOR = 2DVECTOR
    /// </summary>
    /// <param name="origVector"></param>
    /// <returns>Vector2</returns>
    protected virtual Vector2 scalarTimesVector(float scalar, Vector2 origVector)
    {
        origVector[0] *= scalar;
        origVector[1] *= scalar;
        return origVector;
    }
    #endregion

    #region Shooting Decision and Execution
    /// <summary>
    /// shoots if cooldownTimer allows
    /// </summary>
    protected virtual void shoot()
    {
        cooldownTimer = delayFireSec;
        Vector3 offset = transform.rotation * new Vector3(0, 0.75f, 0);
        bulletPrefab.tag = "BaseLaser";
        Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
    }

    /// <summary>
    /// judges if the bot should shoot
    /// </summary>
    protected virtual void shouldShoot()
    {
        cooldownTimer -= Time.deltaTime;

        //find range
        float minDegree = degreeToTarget - 10;
        float maxDegree = degreeToTarget + 10;

        //if within range then shoot
        if (cooldownTimer <= 0 && transform.rotation.eulerAngles.z <= maxDegree && transform.rotation.eulerAngles.z >= minDegree)
        {
            shoot();
        }
    }

    /// <summary>
    /// Plays shooting sound
    /// </summary>
    protected virtual void playShootingSound()
    {
    }
    #endregion

    /// <summary>
    /// Take damage on collision
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        health -= damageDictionary.damages[collision.gameObject.tag];
        if (health <= 0)
        {
            isBoom = true;
            //make intangible explosion
            GetComponent<SpriteRenderer>().sprite = boom;
            Destroy(GetComponent<BoxCollider2D>());
            return;
        }
        isBackingUp = true;
        recentCollision = collision.gameObject.GetComponent<Rigidbody2D>();
    }
}