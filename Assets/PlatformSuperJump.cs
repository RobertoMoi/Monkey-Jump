using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSuperJump : MonoBehaviour
{
	public float jumpForce = 18f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
			if (rigidbody != null)
			{
				Vector2 velocity = rigidbody.velocity;
				velocity.y = jumpForce;
				rigidbody.velocity = velocity;
			}
		}
	}
}
