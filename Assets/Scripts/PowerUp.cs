using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public GameObject item;

	void OnTriggerEnter2D(Collider2D other)
	{
		Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

		if (rigidbody != null)
		{
			Destroy(item);
		}
	}
}


