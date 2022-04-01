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

	public GameObject newSpawnPoint;
	public GameObject player;

	public GameObject[] LifePoint;

	public int playerLife;

	public int numberOfPlatforms = 200;

	//numero di blocchi che devono essere generati prima di creare i power up
	int platformsBeforePlayerBooster; 
	int platformsBeforeEnemyBooster;
	int platformsBeforeplayerSlowDown;
	int platformsBeforeEnemySlowDown;
	
	//numero di blocchi che devono essere generati prima di creare i blocchi distruttibili e quelli che permettono il super salto
	int platformsBeforeDestructiblePlatform;
	int platformsBeforeSuperJumpPlatform;

	public float levelWidth; //larghezza massima in cui possono essere creati dei blocchi
	public float minY; //altezza minima per la generazione dei blocchi
	public float maxY; //altezza massima per la generazione dei blocchi

	private float newSpawnPositionY; //punto in cui verranno generati i nuovi oggetti
	
	private float platformSpawnVariation; //limita la generazione di blocchi troppo ravvicinati
	private float itemSpawnVariation; //limita la generazione di oggetti troppo ravvicinati

	/*range di massimi e minimi che servono per aggiornare le variabili che contengono il numero di blocchi 
	 * generati prima della creazione dei power up */
	private int minRangePowerUp;
	private int maxRangePowerUp;
	private int minRangeWeakening;
	private int maxRangeWeakening;

	Vector3 spawnPosition = new Vector3(); //vettore che determinerà dove verranno generati i blocchi
	Vector3 itemSpawnPosition = new Vector3(); //vettore che determinerà dove verranno generati i power up

	// Use this for initialization
	void Start()
	{			
		chosenMode = CommonVariablesBetweenScenes.difficultyChoice; //livello di difficoltà scelto dal giocatore nel menù principale

		if (chosenMode == CommonVariablesBetweenScenes.DifficultyLevel.Easy)
		{
			minRangePowerUp = 7;
		    maxRangePowerUp = 23;
	        minRangeWeakening = 11;
	        maxRangeWeakening = 30;

			platformSpawnVariation = 0.5f;
			itemSpawnVariation = 0.6f;

			platformsBeforeDestructiblePlatform = 9;
			platformsBeforeSuperJumpPlatform = 13;

			ItemValuesInitialization(minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);
			
			//Nella modalità facile vengono attivate tutte e 3 le vite
			LifePoint[0].SetActive(true);
			LifePoint[1].SetActive(true);
			LifePoint[2].SetActive(true);
			playerLife = 3;

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, platformSpawnVariation, platformsBeforeDestructiblePlatform, platformsBeforeSuperJumpPlatform);
				ItemGeneration(i, itemSpawnVariation, minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);
			}
		}
		else if(chosenMode == CommonVariablesBetweenScenes.DifficultyLevel.Medium)
        {
			minRangePowerUp = 9;
			maxRangePowerUp = 25;
			minRangeWeakening = 11;
			maxRangeWeakening = 27;

			platformSpawnVariation = 0.7f;
			itemSpawnVariation = 0.8f;

			platformsBeforeDestructiblePlatform = 7;
			platformsBeforeSuperJumpPlatform = 15;

			ItemValuesInitialization(minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);

			//Nella modalità media vengono attivate 2 vite
			LifePoint[0].SetActive(true);
			LifePoint[1].SetActive(true);
			playerLife = 2;
			
			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, platformSpawnVariation, platformsBeforeDestructiblePlatform, platformsBeforeSuperJumpPlatform);
				ItemGeneration(i, itemSpawnVariation, minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);
			}
		}
		else
        {
			minRangePowerUp = 13;
			maxRangePowerUp = 35;
			minRangeWeakening = 8;
			maxRangeWeakening = 22;

			platformSpawnVariation = 0.9f;
			itemSpawnVariation = 1f;

			platformsBeforeDestructiblePlatform = 5;
			platformsBeforeSuperJumpPlatform = 17;

			ItemValuesInitialization(minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);

			//Nella modalità difficile viene attivata 1 sola vita
			LifePoint[0].SetActive(true);
			playerLife = 1;

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, platformSpawnVariation, platformsBeforeDestructiblePlatform, platformsBeforeSuperJumpPlatform);
				ItemGeneration(i, itemSpawnVariation, minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);
			}
		}
	}

	void Update()
    {
		if(player.transform.position.y > newSpawnPoint.transform.position.y)
        {
			ItemValuesInitialization(minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);

			for (int i = 0; i < numberOfPlatforms; i++)
			{
				PlatformGeneration(i, platformSpawnVariation, platformsBeforeDestructiblePlatform, platformsBeforeSuperJumpPlatform);
				ItemGeneration(i, itemSpawnVariation, minRangePowerUp, maxRangePowerUp, minRangeWeakening, maxRangeWeakening);
				Debug.Log(platformsBeforeplayerSlowDown);
			}
			/*Il nuovo punto di spawn dei blocchi sarà dato dalla posizione dell'ultimo blocco generato meno una quantità che serve 
			 * per non creare dei punti privi di blocchi */
			newSpawnPositionY = spawnPosition.y - 20f; 
			newSpawnPoint.transform.position = new Vector3(0, newSpawnPositionY, 0);
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
		//permette una variazione quando la funzione random delle variabili qui sotto produce lo stesso numero
		int spawnVariation = Random.Range(-5, 5); 

		platformsBeforePlayerBooster = Random.Range(minRangePowerUp, maxRangePowerUp);	
		platformsBeforeEnemySlowDown = Random.Range(minRangePowerUp, maxRangePowerUp) + spawnVariation;

		platformsBeforeEnemyBooster = Random.Range(minRangeWeakening, maxRangeWeakening);
		platformsBeforeplayerSlowDown = Random.Range(minRangeWeakening, maxRangeWeakening) + spawnVariation;
	}
}