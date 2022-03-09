using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestructible : MonoBehaviour
{
	public float jumpForce = 11f;
	public int points = 3;
	public Manager manager;
	public bool firstCollision = true;
	public bool isColliding = false;

	private Animator animator;

	void Start()
	{
		manager = GameObject.Find("GameManager").GetComponent<Manager>();
		animator = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
			if (rigidbody != null)
			{
				isColliding = true;
				animator.SetBool("isColliding", isColliding);
				isColliding = false;
				if (firstCollision)
				{
					manager.UpdateScore(points);
					firstCollision = false;
				}

				Vector2 velocity = rigidbody.velocity;
				velocity.y = jumpForce;
				rigidbody.velocity = velocity;
			}
		}
	}
}
