using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float jumpForce = 10f;
	private GameObject boundary;

	void Start()
    {
		boundary = GameObject.Find("BottomOfGame");
    }
	// Get notified whenever something bounces on a platform
	// When object collides with a platform character bounces
	void OnCollisionEnter2D(Collision2D collision)
	{
		// Check whether the object is coming from below or above
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

			// If we find a rigidbody on the object the character collided with
			if (rb != null)
			{
				// Then add force

				// Modify the velocity of character's rigidbody directly
				Vector2 velocity = rb.velocity;																// Getting the vector

				// Control the y velocity of the character after it hits a platform
				velocity.y = jumpForce;																		// Modifying a component of the vector

				rb.velocity = velocity;																		// Setting it back to this vector
			}
		}
	}

	void Update()
    {
		// destroys platform when it reaches world bottom of the game
		if (transform.position.y < boundary.transform.position.y)
        {
			Destroy(gameObject);
        }
    }
}
