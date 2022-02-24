using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public GameObject platformPrefab;
	public GameObject destructiblePlatform;
	public GameObject superjumpPlatform;

	public GameObject playerSpeedBooster;
	public GameObject enemySpeedBooster;
	public GameObject playerSlowDown;
	public GameObject enemySlowDown;

	public int numberOfPlatforms = 200;

	public int platformsBeforePlayerBooster = 15;
	public int platformsBeforeEnemyBooster = 25;
	public int platformsBeforeplayerSlowDown = 20;
	public int platformsBeforeEnemySlowDown = 12;
	
	public float levelWidth = 4f;
	public float minY = .5f;
	public float maxY = 3f;


	// Use this for initialization
	void Start()
	{

		Vector3 spawnPosition = new Vector3();
		Vector3 itemSpawnPosition = new Vector3();

		for (int i = 0; i < numberOfPlatforms; i++)
		{
			spawnPosition.y = spawnPosition.y + Random.Range(minY, maxY) + 0.5f;
			spawnPosition.x = Random.Range(-levelWidth, levelWidth);
			
			itemSpawnPosition.y = itemSpawnPosition.y + Random.Range(minY, maxY) + 1.7f;
			itemSpawnPosition.x = Random.Range(-levelWidth, levelWidth);

			if (i % 5 == 0)
			{
				Instantiate(destructiblePlatform, spawnPosition, Quaternion.identity);
			}
			else if (i % 11 == 0)
			{
				Instantiate(superjumpPlatform, spawnPosition, Quaternion.identity);
			}
			else
			{
				Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
			}

			if(i == platformsBeforePlayerBooster)
            {
				Instantiate(playerSpeedBooster, itemSpawnPosition, Quaternion.identity);
				platformsBeforePlayerBooster += 15;
			}
			else if(i == platformsBeforeEnemyBooster)
            {
				itemSpawnPosition.y -= .3f;

				Instantiate(enemySpeedBooster, itemSpawnPosition, Quaternion.identity);
				platformsBeforeEnemyBooster += 25;
			}
			else if (i == platformsBeforeplayerSlowDown)
			{
				itemSpawnPosition.y += 1f;

				Instantiate(playerSlowDown, itemSpawnPosition, Quaternion.identity);
				platformsBeforeplayerSlowDown += 20;
			}
			else if (i == platformsBeforeEnemySlowDown)
			{
				itemSpawnPosition.y += .4f;

				Instantiate(enemySlowDown, itemSpawnPosition, Quaternion.identity);
				platformsBeforeEnemySlowDown += 12;
			}

		}

	}
}