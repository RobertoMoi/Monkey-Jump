using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public CommonVariablesBetweenScenes.DifficultyLevel currentDifficulty;
    public float speed;
    public float normalSpeed;
    public float stoppingTimeWhenCollide; //tempo in cui il nemico deve stare fermo dopo che ha colpito il giocatore
    public float stoppingDistanceFromPlayer; //distanza massima che può raggiungere dal giocatore prima di fermarsi
    public Rigidbody2D rb;

    private GameObject target;
   
    // Start is called before the first frame update
    void Start()
    {
        SetSpeed();
        //Il giocatore viene impostato come obiettivo da seguire
        target = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        //se il giocatore non esiste non fare nulla
        if (target == null)
            return;

        //se il nemico non ha raggiunto la distanza massima raggiungibile dal giocatore allora lo insegue
        if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistanceFromPlayer) { 
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void  OnCollisionEnter2D(Collision2D collision)
    {
        //Quando entra in collisione con il giocatore il nemico si ferma per dare il tempo al giocatore di reagire e allontanarsi
        if(collision.gameObject.tag == "Player")
        {
            speed = 0f;
            StartCoroutine("StoppingEnemy");
        }
    }
    
    void SetSpeed()
    {
        currentDifficulty = CommonVariablesBetweenScenes.difficultyChoice; //prende la difficoltà scelta dal giocatore nel menù principale

        //Più il livello di difficoltà è elevato e più il nemico è veloce
        if (currentDifficulty == CommonVariablesBetweenScenes.DifficultyLevel.Easy)
        {
            speed -= 1;
            normalSpeed -= 1;
        }
        else if (currentDifficulty == CommonVariablesBetweenScenes.DifficultyLevel.Medium)
        {
            speed -= .5f;
            normalSpeed -= .5f;
        }
    }
    
    //Ferma il nemico per un tot di secondi per poi riprendere la velocità normale
    IEnumerator StoppingEnemy()
    {
        yield return new WaitForSeconds(stoppingTimeWhenCollide);
        speed = normalSpeed;
    }
}
