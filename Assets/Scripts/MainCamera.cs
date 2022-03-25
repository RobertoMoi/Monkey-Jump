using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public Transform target;
	//permette di seguire correttamente il giocatore durante la partita poiché viene chiamata dopo l'update del giocatore
	void LateUpdate()
	{
		//la camera si aggiorna quando il giocatore avanza nell'asse y e la sua posizione è maggiore rispetto all'asse y della camera
		if (target.position.y > transform.position.y)
		{
			Vector3 newPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
			transform.position = newPosition;
		}
	}
}
