using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		//set the position of the camera's to be the same as the player's
	    if (player != null)
	    {
	        transform.position = player.transform.position + offset;
	    }
	}
}
