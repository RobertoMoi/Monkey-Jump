using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public GameObject platformPrefab;
	public GameObject platformDestructible;
	public GameObject platformSuperJump;

	public int numberOfPlatforms = 200;
	public float levelWidth = 3f;
	public float minY = .5f;
	public float maxY = 3f;
	public float minRandom = .1f;
	public float maxRandom = .3f;
	public float n = 0;

	// Use this for initialization
	void Start()
	{

		Vector3 spawnPosition = new Vector3();
		Vector3 spawnPositionPlatformDestructible = new Vector3();
		Vector3 spawnPositionPlatformSuperJump = new Vector3();
		

		for (int i = 0; i < numberOfPlatforms; i++)
		{
			n = Random.Range(minRandom, maxRandom);
			n += 
			spawnPosition.y = spawnPosition.y + Random.Range(minY, maxY) + 0.5f;;
			spawnPosition.x = Random.Range(-levelWidth, levelWidth);
			

			if (i % 5 == 0)
			{
				
				Instantiate(platformDestructible, spawnPosition, Quaternion.identity);
			}
			else
			{
				
				Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
			}
			
		}

	}
}