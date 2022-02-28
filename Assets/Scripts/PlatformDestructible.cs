using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestructible : MonoBehaviour
{
	public float jumpForce = 11f;
	public GameObject platform;
	public Manager manager;
	public bool firstCollision = true;

	void Start()
	{
		manager = GameObject.Find("GameManager").GetComponent<Manager>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
			if (rigidbody != null)
			{
				if (firstCollision)
				{
					manager.UpdateScore();
					firstCollision = false;
				}

				Vector2 velocity = rigidbody.velocity;
				velocity.y = jumpForce;
				rigidbody.velocity = velocity;
				Destroy(platform);
			}
		}
	}
}
