using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Rigidbody component will always maintain on the player
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	public float movementSpeed = 10f;

	Rigidbody2D rb;

	float movement = 0f;

	public Animator animator;

	// Click to Start
	private bool isStarted = false;
	public TextMeshProUGUI startText;

	// SCORE
	public TextMeshProUGUI scoreText;
	public static float topScore = 0.0f;

	//Variables used for maintaing the horizontal bounds of player movement
	[SerializeField] public float gameWorldBounds;
	public float centerScreen;
	[SerializeField] public GameObject leftWall, rightWall, bottomOfGame;

	//Variable representing the initial distance from the player starting position to the bottom of screen
	private Vector2 initialOffset;

	[SerializeField] GameObject winScreen;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.velocity = Vector3.zero;

		//Center of screen(player start) is found
		centerScreen = gameObject.transform.position.x;

		//Initial offset distance between player and bottom of the game is retrieved
		initialOffset = bottomOfGame.transform.position - transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		// Click to Start
		if(Input.GetMouseButtonDown(0) && isStarted == false)
        {
			isStarted = true;
			startText.gameObject.SetActive(false);
			rb.gravityScale = 1f;
        }

		if (isStarted == true)
        {
			// Get HorizontalMovement Input
			if (gameObject.transform.position.x < gameWorldBounds + centerScreen &&
				gameObject.transform.position.x > -gameWorldBounds + centerScreen)
					movement = Input.GetAxis("Horizontal") * movementSpeed;
			
			// SCORE
			// if we're moving upwards and our current positon is greater than topScore
			if (rb.velocity.y > 0 && transform.position.y > topScore)
			{
				// then we can update topScore to our current position
				topScore = transform.position.y;
			}

			// always update the score text
			scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
		}
	}

	// Movement
	void FixedUpdate()
	{
		if (isStarted == true)
        {
			// Change x velocity
			Vector2 velocity = rb.velocity;
			velocity.x = movement;
			rb.velocity = velocity;

			//Wall Moves
			moveWalls(velocity);
			moveBottomOfGame();
		}
	}

	private void moveWalls(Vector2 velocity)
	{
		leftWall.GetComponent<Rigidbody2D>().velocity = velocity;
		rightWall.GetComponent<Rigidbody2D>().velocity = velocity;
	}

	private void moveBottomOfGame()
	{
		if(rb.velocity.y > .01)
		{
			bottomOfGame.transform.position = (Vector2)transform.position + initialOffset;
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
			//animator.Play("jump");

			animator.SetTrigger("JumpTrigger");
        }

		if(collision.gameObject.tag == "EndPlatform")
        {
			winScreen.SetActive(true);
			Debug.Log("game over, final hit");
        }
    }

}

