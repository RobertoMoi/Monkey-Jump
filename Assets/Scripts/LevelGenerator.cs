using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private CommonVariablesBetweenScenes.DifficultyLevel chosenMode;

	public GameObject platformPrefab;
	public GameObject destructiblePlatform;
	public GameObject superjumpPlatform;

	public GameObject playerSpeedBooster;
	public GameObject enemySpeedBooster;
	public GameObject playerSlowDown;
	public GameObject enemySlowDown;

	public GameObject[] LifePoint;

	public int playerLife;

	public int numberOfPlatforms = 200;

	//numero di blocchi che devono essere generati prima di creare i power up
	int platformsBeforePlayerBooster; 
	int platformsBeforeEnemyBooster;
	int platformsBeforeplayerSlowDown;
	int platformsBeforeEnemySlowDown;

	public float levelWidth = 4f; //larghezza massima in cui possono essere creati dei blocchi
	public float minY = .4f; //altezza minima per la generazione dei blocchi
	public float maxY = 2.5f; //altezza massima per la generazione dei blocchi

	/*range di massimi e minimi che servono per aggiornare le variabili che contengono il numero di blocchi 
	 * generati prima della creazione dei power up */
	public int minRangePowerUp; 
	public int maxRangePowerUp;
	public int minRangeWeakening;
	public int maxRangeWeakening;

	Vector3 spawnPosition = new Vector3(); //vettore che determinerà dove verranno generati i blocchi
	Vector3 itemSpawnPosition = new Vector3(); //vettore che determinerà dove verranno generati i power up

	// Use this for initialization
	void Start()
	{			
		chosenMode = CommonVariablesBetweenScenes.difficultyChoice; //livello di difficoltà scelto dal giocatore nel menù principale

		if (chosenMode == CommonVariablesBetweenScenes.DifficultyLevel.Easy)
		{
			ItemValuesInitialization(6, 23, 12, 36);
			
			//Nella modalità facile vengono attivate tutti e 3 le vite
			LifePoint[0].SetActive(true);
			LifePoint[1].SetActive(true);
			LifePoint[2].SetActive(true);
			playerLife = 3;

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 0.5f, 9, 13);
				ItemGeneration(i, 0.2f, 5, 20, 15, 40);
			}
		}
		else if(chosenMode == CommonVariablesBetweenScenes.DifficultyLevel.Medium)
        {
			ItemValuesInitialization(12, 30, 8, 25);

			//Nella modalità media vengono attivate 2 vite
			LifePoint[0].SetActive(true);
			LifePoint[1].SetActive(true);
			playerLife = 2;
			
			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 1f, 5, 17);
				ItemGeneration(i, 1f, 8, 25, 10, 30);
			}
		}
		else
        {
			ItemValuesInitialization(21, 43, 6, 20);

			//Nella modalità difficile viene attivata 1 sola vita
			LifePoint[0].SetActive(true);
			playerLife = 1;

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, 1.7f, 5, 29);
				ItemGeneration(i, 1.2f, 18, 38, 5, 25);
			}
		}
	}

	/* Permette la generazione di blocchi
	 * i indice del ciclo
	 * platformSpawnVariation limita generazione di blocchi ravvicinati
	 * platformsBeforeDestructiblePlatform blocchi da generare prima di generare un platform distruttibile
	 * platformsBeforeSuperJumpPlatform blocchi da generare prima di generare un platform con super salto */
	void PlatformGeneration(int i, float platformSpawnVariation, int platformsBeforeDestructiblePlatform, int platformsBeforeSuperJumpPlatform)
	{
		/*generazione random della posizione dei blocchi 
		 * con l'aggiunta di una variabile per limitare la generazione di blocchi troppo vicini o sovrapposti */
		spawnPosition.y = spawnPosition.y + Random.Range(minY, maxY) + platformSpawnVariation;
		spawnPosition.x = Random.Range(-levelWidth, levelWidth);

		/*tramite il calcolo del modulo viene impostato ogni quanto deve essere generato 
		 * un blocco distruttibile, uno con il super salto o uno di default */
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

	/* Permette la generazione degli oggetti
	 * i indice del ciclo
	 * itemSpawnVariation limita generazione di oggetti ravvicinati
	 * minPowerUpSpawn range minimo di blocchi da generare prima di generare un power up
	 * maxPowerUpSpawn range massimo di blocchi da generare prima di generare un power up 
	 * minWeakeningSpawn range minimo di blocchi da generare prima di generare un oggetto da evitare
	 * maxWeakeningSpawn range massimo di blocchi da generare prima di generare un oggetto da evitare
	 */
	void ItemGeneration(int i, float itemSpawnVariation, int minPowerUpSpawn, int maxPowerUpSpawn, int minWeakeningSpawn, int maxWeakeningSpawn)
    {
		itemSpawnPosition.y = itemSpawnPosition.y + Random.Range(minY, maxY) + itemSpawnVariation;
		itemSpawnPosition.x = Random.Range(-levelWidth, levelWidth);

		/* ogni volta che viene generato un oggetto viene aggiornata (in maniera randomica con due range min e max) la variabile che contiene 
		 * il numero di blocchi che devono essere generati prima di creare un nuovo oggetto */
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

	//inzializza in maniera randomica il numero di blocchi che devono essere generati prima di creare un nuovo oggetto
	void ItemValuesInitialization(int minRangePowerUp, int maxRangePowerUp, int minRangeWeakening, int maxRangeWeakening)
    {
		platformsBeforePlayerBooster = Random.Range(minRangePowerUp, maxRangePowerUp);	
		platformsBeforeEnemySlowDown = Random.Range(minRangePowerUp, maxRangePowerUp);

		platformsBeforeEnemyBooster = Random.Range(minRangeWeakening, maxRangeWeakening);
		platformsBeforeplayerSlowDown = Random.Range(minRangeWeakening, maxRangeWeakening);
	}
}