using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	public GameObject pauseMenu;

	public GameObject enemy;
	EnemyController enemyScript;
	public GameObject mainCamera;
	private Manager _manager;
	private LevelGenerator _levelGenerator;

	public int life;

	public float speed = 30f;
	public float boostedSpeed = 15f;
	public float normalSpeed = 10f;
	public float slowedSpeed = 6f;
	
	public float slowedEnemySpeed = 1;
	public float boostedEnemySpeed = 1;

	public float changedSpeedTime = 15f;
	public float playerHitTime = 3f;

	private Animator animator;

	private bool boosterCollision = false;
	private bool insecticideCollision = false;
	private bool poisonCollision = false;
	private bool hiveCollision = false;
	private bool hitByEnemy = false;

	private bool boosterCoroutineIsRunning = false;
	private bool slowedEnemyCoroutineIsRunning = false;
	private bool boosterEnemyCoroutineIsRunning = false;
	private bool slowedPlayerCoroutineIsRunning = false;

	Rigidbody2D rb;

	private float belowCameraPosition = -7f;

	float control = 0f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyScript = enemy.GetComponent<EnemyController>();
		animator = GetComponent<Animator>();
		_manager = GameObject.Find("GameManager").GetComponent<Manager>();
		_levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		
		life = _levelGenerator.playerLife; //numero di vite del giocatore in base alla difficoltà scelta
	}

	// Update is called once per frame
	void Update()
	{
		control = Input.GetAxis("Horizontal") * speed;

		//vengono impostate le variabili relative alle varie animazioni del giocatore
		animator.SetBool("collision", boosterCollision);
		animator.SetBool("insecticideCollision", insecticideCollision);
		animator.SetBool("poisonCollision", poisonCollision);
		animator.SetBool("hiveCollision", hiveCollision);
		animator.SetBool("hit", hitByEnemy);
		
		//Quando il giocatore cade e finisce sotto la visuale della camera la partita termina
		if ((transform.position.y  - mainCamera.transform.position.y) <= belowCameraPosition)
		{
			//Destroy(gameObject);
			control = 0; //toglie la possibilità al giocatore di potersi muovere dopo che la partita è finita
			_manager.saveData(); //salva i dati correnti
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //carica la scena di game over
		}

		//cliccando il tasto esc si attiverà il menù di pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = control;
		rb.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D other)
    {	
		//il giocatore viene colpito dal nemico
		if(other.gameObject.CompareTag("Enemy"))
        {	
			//inizia l'animazione del giocatore colpito dal nemico per un determinato tempo
			hitByEnemy = true;
			StartCoroutine("hitPlayerAnimation");

			/*se il giocatore ha ancora vite vengono decrementate e viene distrutto l'indicatore di vita appropriato (quello più a destra)
			 * altrimenti vengono salvati i dati e la partita termina */
			if (life > 0)
			{
				life--;
				Destroy(_levelGenerator.LifePoint[life]);
			}
			else
			{
				control = 0;
				_manager.saveData();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
    }

	// gestisce i casi in cui il giocatore entra in contatto con gli oggetti
	void OnTriggerEnter2D(Collider2D other)
    {

		/*ogni volta che il giocatore entra in contatto con un oggetto parte l'animazione appropriata per un determinato periodo di tempo per poi
		 * ritornare in idle */
        if (other.gameObject.CompareTag("SpeedBooster"))
        {
			//se il giocatore ha già attivato lo speed booster viene disattivato per far ripartire la coroutine da zero
			if(boosterCoroutineIsRunning)
				StopCoroutine("ChangedSpeedDuration");

			//se il giocatore ha preso l'oggetto che lo rallenta viene disattivato il suo effetto e la sua animazione
			if (slowedPlayerCoroutineIsRunning)
			{
				poisonCollision = false;
				StopCoroutine("SlowedSpeedDuration");
			}

			//attiva la coroutine dello speed booster e la sua animazione
			boosterCollision = true;
			speed = boostedSpeed;
			StartCoroutine("ChangedSpeedDuration");			
        }
		
		//lavora in maniera simile allo speed booster ma in questo caso il giocatore entra in contatto con l'oggetto che lo rallenta
		if (other.gameObject.CompareTag("PlayerSlowDown"))
		{
			if (boosterCoroutineIsRunning)
			{
				boosterCollision = false;
				StopCoroutine("ChangedSpeedDuration");
			}

			if (slowedPlayerCoroutineIsRunning)
				StopCoroutine("SlowedSpeedDuration");

			poisonCollision = true;
			speed = slowedSpeed;
			StartCoroutine("SlowedSpeedDuration");
		}

		//lavora in maniera simile allo speed booster ma in questo caso il giocatore entra in contatto con l'oggetto che rallenta il nemico
		if (other.gameObject.CompareTag("EnemySlowDown"))
		{
			if(slowedEnemyCoroutineIsRunning)
				StopCoroutine("SlowedEnemySpeedDuration");

            if (boosterEnemyCoroutineIsRunning)
            {
				hiveCollision = false;
				StopCoroutine("BoostedEnemySpeedDuration");
            }
				
			insecticideCollision = true;
			enemyScript.speed -= slowedEnemySpeed; //la velocità del nemico viene diminuita
			StartCoroutine("SlowedEnemySpeedDuration");
		}

		//lavora in maniera simile allo speed booster ma in questo caso il giocatore entra in contatto con l'oggetto che velocizza il nemico
		if (other.gameObject.CompareTag("EnemyBooster"))
		{
			if (slowedEnemyCoroutineIsRunning)
            {
				insecticideCollision = false;
				StopCoroutine("SlowedEnemySpeedDuration");
            }
				
			if (boosterEnemyCoroutineIsRunning)
				StopCoroutine("BoostedEnemySpeedDuration");

			hiveCollision = true;
			enemyScript.speed += boostedEnemySpeed; //la velocità del nemico viene aumentata
			StartCoroutine("BoostedEnemySpeedDuration");
		}

	}

	/*queste coroutine prima attivano le animazioni e gli effetti degli oggetti che vengono presi dal giocatore e successivamente,
	 * dopo un tot di tempo, riportano l'animazione del giocatore in idle e disattivano gli effetti dei vari oggetti */
	IEnumerator ChangedSpeedDuration()
    {
		boosterCoroutineIsRunning = true;
		yield return new WaitForSeconds(changedSpeedTime);
		boosterCoroutineIsRunning = false;
		speed = normalSpeed;
		boosterCollision = false;
		poisonCollision = false;
	}

	IEnumerator SlowedSpeedDuration()
	{
		slowedPlayerCoroutineIsRunning = true;
		yield return new WaitForSeconds(changedSpeedTime);
		slowedPlayerCoroutineIsRunning = false;
		speed = normalSpeed;
		poisonCollision = false;
	}

	IEnumerator SlowedEnemySpeedDuration()
	{
		slowedEnemyCoroutineIsRunning = true;
		yield return new WaitForSeconds(changedSpeedTime);
		slowedEnemyCoroutineIsRunning = false;
		insecticideCollision = false;
		enemyScript.speed = enemyScript.normalSpeed;
	}

	IEnumerator BoostedEnemySpeedDuration()
	{
		boosterEnemyCoroutineIsRunning = true;
		yield return new WaitForSeconds(changedSpeedTime);
		hiveCollision = false;
		boosterEnemyCoroutineIsRunning = false;
		enemyScript.speed = enemyScript.normalSpeed;
	}

	IEnumerator hitPlayerAnimation()
    {
		yield return new WaitForSeconds(playerHitTime);
		hitByEnemy = false;
	}
}
