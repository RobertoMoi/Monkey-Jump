using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float stoppingDistanceFromPlayer;
    public Rigidbody2D rb;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player");   }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistanceFromPlayer) { 
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
        }
    }
}
