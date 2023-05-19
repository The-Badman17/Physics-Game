using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script generates entire level at one time
public class LevelGenerator : MonoBehaviour
{
	public GameObject platformPrefab;

	public int numberOfPlatforms = 200;       // Number of platforms we want to spawn
	public float levelWidth = 2f;
	public float minY = .2f;
	public float maxY = 1.5f;

	[SerializeField]GameObject firstPlatform;
	[SerializeField] GameObject endingPlatform;

	// Start is called before the first frame update
	// Randomly Spawn Platforms
	void Start()
	{
		// Create Vector3 and set it to a new Vector3 by default
		Vector2 spawnPosition = new Vector2(firstPlatform.transform.position.x, firstPlatform.transform.position.y);
		Vector2 ogPosition = spawnPosition;	//Represents the starting position of the first platform (center screen)

		// Only generate platforms up to the number we specify
		for (int i = 0; i < numberOfPlatforms; i++)
		{
			spawnPosition = ogPosition + new Vector2(0,spawnPosition.y);

			// Increase the spawn position on the Y by a random value
			spawnPosition.y += Random.Range(minY, maxY);
			spawnPosition.x += Random.Range(-levelWidth, levelWidth);

			// Instantiate a platform at spawnPosition then loop through again
			Instantiate(platformPrefab, spawnPosition, Quaternion.identity); // Quaternion.identity prevents object from rotating

			//End of game platform is instantiated
			if (i == numberOfPlatforms - 1)
			{
				spawnPosition.y += maxY;
				spawnPosition.x += 0;
				Instantiate(endingPlatform, spawnPosition, Quaternion.identity);
			}
		}
	}
}
