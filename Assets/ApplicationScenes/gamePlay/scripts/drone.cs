using UnityEngine;

public class drone : baseBot
{

    public float acceleration = 100;
    public float botHealth = 10;

    /// <summary>
    /// Changes default health
    /// </summary>
    protected override void Start()
    {
        base.Start();
        health = botHealth;
    }

    /// <summary>
    /// Does not slow down to avoid collisions
    /// </summary>
    protected override void changeVelocity()
    {
        rotate();
        //move drone
        float frameStep = maxVelocity * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, frameStep);
    }

    /// <summary>
    /// Point directly at the ship
    /// </summary>
    /// <returns></returns>
    protected override Quaternion rotate()
    {
        // Get the Quaternion
        Quaternion rot = transform.rotation;

        float degreeToTarget = findDegree(transform.position - target.transform.position);

        transform.rotation = Quaternion.Euler(0, 0, degreeToTarget);

        return transform.rotation;
    }

    /// <summary>
    /// Don't shoot. This drone is just a kamikaze
    /// </summary>
    protected override void shouldShoot()
    {
        return;
    }
}
