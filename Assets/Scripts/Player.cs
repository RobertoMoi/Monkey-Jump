using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{	
	public GameObject enemy;
	EnemyController enemyScript;
	
	public float speed = 30f;
	public float boostedSpeed = 15f;
	public float normalSpeed = 10f;
	public float slowedSpeed = 6f;
	
	public float slowedEnemySpeed = 3f;
	public float boostedEnemySpeed = 2f;

	public float changedSpeedTime = 15;
	
	Rigidbody2D rb;

	float control = 0f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyScript = enemy.GetComponent<EnemyController>();
	}

	// Update is called once per frame
	void Update()
	{
		control = Input.GetAxis("Horizontal") * speed;
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = control;
		rb.velocity = velocity;
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpeedBooster"))
        {
			speed = boostedSpeed;
			StartCoroutine("ChangedSpeedDuration");
        }
		
		if (other.gameObject.CompareTag("PlayerSlowDown"))
		{
			speed = slowedSpeed;
			StartCoroutine("ChangedSpeedDuration");
		}

		if (other.gameObject.CompareTag("EnemySlowDown"))
		{
			enemyScript.speed -= slowedEnemySpeed;
			StartCoroutine("SlowedEnemySpeedDuration");
		}

		if (other.gameObject.CompareTag("EnemyBooster"))
		{
			enemyScript.speed += boostedEnemySpeed;
			StartCoroutine("BoostedEnemySpeedDuration");
		}

	}

	IEnumerator ChangedSpeedDuration()
    {
		yield return new WaitForSeconds(changedSpeedTime);
		speed = normalSpeed;
    }

	IEnumerator SlowedEnemySpeedDuration()
	{
		yield return new WaitForSeconds(changedSpeedTime);
		enemyScript.speed += slowedEnemySpeed;
	}

	IEnumerator BoostedEnemySpeedDuration()
	{
		yield return new WaitForSeconds(changedSpeedTime);
		enemyScript.speed -= boostedEnemySpeed;
	}

}
