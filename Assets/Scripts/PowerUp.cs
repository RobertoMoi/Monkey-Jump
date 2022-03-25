using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public GameObject item;

	//quando il giocatore entra in contatto con un oggetto questo viene distrutto
	void OnTriggerEnter2D(Collider2D other)
	{
		Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

		if (rigidbody != null)
		{
			Destroy(item);
		}
	}
}


