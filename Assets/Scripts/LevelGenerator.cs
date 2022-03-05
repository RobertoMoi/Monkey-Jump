using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public Difficulty.DifficultyLevel chosenMode;

	public GameObject platformPrefab;
	public GameObject destructiblePlatform;
	public GameObject superjumpPlatform;

	public GameObject playerSpeedBooster;
	public GameObject enemySpeedBooster;
	public GameObject playerSlowDown;
	public GameObject enemySlowDown;

	public int numberOfPlatforms = 200;

	int platformsBeforePlayerBooster;
	int platformsBeforeEnemyBooster;
	int platformsBeforeplayerSlowDown;
	int platformsBeforeEnemySlowDown;

	public float levelWidth = 4f;
	public float minY = .4f;
	public float maxY = 2.5f;

	public int minRangePowerUp;
	public int maxRangePowerUp;
	public int minRangeWeakening;
	public int maxRangeWeakening;

	Vector3 spawnPosition = new Vector3();
	Vector3 itemSpawnPosition = new Vector3();

	// Use this for initialization
	void Start()
	{
		chosenMode = Difficulty.difficultyChoice;

		if (chosenMode == Difficulty.DifficultyLevel.Easy)
		{
			ItemValuesInitialization(6, 23, 12, 36);

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 0.5f, 9, 13);
				ItemGeneration(i, 0.2f, 5, 20, 15, 40);
			}
		}
		else if(chosenMode == Difficulty.DifficultyLevel.Medium)
        {
			ItemValuesInitialization(12, 30, 8, 25);

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 1f, 5, 17);
				ItemGeneration(i, 1f, 8, 25, 10, 30);
			}
		}
		else
        {
			ItemValuesInitialization(21, 43, 6, 20);

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 1.7f, 5, 29);
				ItemGeneration(i, 1.2f, 18, 38, 5, 25);
			}
		}
	}

	void PlatformGeneration(int i, float platformSpawnVariation, int platformsBeforeDestructiblePlatform, int platformsBeforeSuperJumpPlatform)
	{
		spawnPosition.y = spawnPosition.y + Random.Range(minY, maxY) + platformSpawnVariation;
		spawnPosition.x = Random.Range(-levelWidth, levelWidth);

		if (i % platformsBeforeDestructiblePlatform == 0)
		{
			Instantiate(destructiblePlatform, spawnPosition, Quaternion.identity);
		}
		else if (i % platformsBeforeSuperJumpPlatform == 0)
		{
			Instantiate(superjumpPlatform, spawnPosition, Quaternion.identity);
		}
		else
		{
			Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
		}				
	}

	void ItemGeneration(int i, float itemSpawnVariation, int minPowerUpSpawn, int maxPowerUpSpawn, int minWeakeningSpawn, int maxWeakeningSpawn)
    {
		itemSpawnPosition.y = itemSpawnPosition.y + Random.Range(minY, maxY) + itemSpawnVariation;
		itemSpawnPosition.x = Random.Range(-levelWidth, levelWidth);

		if (i == platformsBeforePlayerBooster)
		{
			Instantiate(playerSpeedBooster, itemSpawnPosition, Quaternion.identity);
			platformsBeforePlayerBooster = platformsBeforePlayerBooster + Random.Range(minPowerUpSpawn, maxPowerUpSpawn);
		}
		else if (i == platformsBeforeEnemyBooster)
		{
			itemSpawnPosition.y -= Random.Range(minY, maxY);

			Instantiate(enemySpeedBooster, itemSpawnPosition, Quaternion.identity);
			platformsBeforeEnemyBooster += Random.Range(minWeakeningSpawn, maxWeakeningSpawn);
		}
		else if (i == platformsBeforeplayerSlowDown)
		{
			itemSpawnPosition.y += Random.Range(minY, maxY);
			
			Instantiate(playerSlowDown, itemSpawnPosition, Quaternion.identity);
			platformsBeforeplayerSlowDown += Random.Range(minWeakeningSpawn, maxWeakeningSpawn);
		}

		else if (i == platformsBeforeEnemySlowDown)
		{ 
			itemSpawnPosition.y += Random.Range(minY, maxY);
			
			Instantiate(enemySlowDown, itemSpawnPosition, Quaternion.identity);
			platformsBeforeEnemySlowDown += Random.Range(minPowerUpSpawn, maxPowerUpSpawn);
		}	
	}

	void ItemValuesInitialization(int minRangePowerUp, int maxRangePowerUp, int minRangeWeakening, int maxRangeWeakening)
    {
		platformsBeforePlayerBooster = Random.Range(minRangePowerUp, maxRangePowerUp);	
		platformsBeforeEnemySlowDown = Random.Range(minRangePowerUp, maxRangePowerUp);

		platformsBeforeEnemyBooster = Random.Range(minRangeWeakening, maxRangeWeakening);
		platformsBeforeplayerSlowDown = Random.Range(minRangeWeakening, maxRangeWeakening);
	}
}