using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// Variable that references our player
	public Transform target;

	void LateUpdate()
	{
		// Check if our target's y value is greater than the camera's y value
		if (target.position.y > transform.position.y)
		{
			// Move camera
			Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
			transform.position = newPos;
		}
	}
}

