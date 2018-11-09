﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// target will = the player
	public GameObject target;

	// targetPosition = the player's x, y
	private Vector3 targetPosition;

	public float smoothing;

	// distance of camera to player
	public float followAhead;

	public bool bounds;

	public Vector2 minCameraPos;
	public Vector2 maxCameraPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y,-5f);

		if (target.transform.localScale.x > 0f) {
			// facing right
			targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, -5);
		} else {
			// facing left
			targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, -5);
		}

		//transform.position = targetPosition;

		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);

		if (bounds)
		{

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y));
		}

	}
}
