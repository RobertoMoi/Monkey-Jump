using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestructible : MonoBehaviour
{
	public float jumpForce = 11f;
	public GameObject platformDestructible;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
			if (rigidbody != null)
			{
				Destroy(gameObject);
			}
		}
	}
}
