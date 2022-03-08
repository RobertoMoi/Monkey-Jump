using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Difficulty.DifficultyLevel currentDifficulty;
    public float speed;
    public float normalSpeed;
    public float stoppingTimeWhenCollide;
    public float stoppingDistanceFromPlayer;
    public Rigidbody2D rb;

    private GameObject target;
   
    // Start is called before the first frame update
    void Start()
    {
        SetSpeed();
        target = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistanceFromPlayer) { 
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void  OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            speed = 0f;
            StartCoroutine("StoppingEnemy");
        }
    }
    
    void SetSpeed()
    {
        currentDifficulty = Difficulty.difficultyChoice;

        if (currentDifficulty == Difficulty.DifficultyLevel.Easy)
        {
            speed -= 1;
            normalSpeed -= 1;
        }
        else if (currentDifficulty == Difficulty.DifficultyLevel.Medium)
        {
            speed -= .5f;
            normalSpeed -= .5f;
        }
    }
  
    IEnumerator StoppingEnemy()
    {
        yield return new WaitForSeconds(stoppingTimeWhenCollide);
        speed = normalSpeed;
    }
}
