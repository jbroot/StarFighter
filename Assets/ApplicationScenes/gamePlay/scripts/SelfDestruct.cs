using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public GameObject bulletPrefab;

    public float timer = 2f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(bulletPrefab);
        }
    }
    /// <summary>
    /// Destroy on collision after 1-3 frames
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        timer = 2f * Time.deltaTime;
    }
}