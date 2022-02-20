using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float jumpHeight = 11f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
			if (rigidbody != null)
			{
				Vector2 velocity = rigidbody.velocity;
				velocity.y = jumpHeight;
				rigidbody.velocity = velocity;
			}
		}
	}
}
