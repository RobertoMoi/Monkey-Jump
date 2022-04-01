using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Gestisce la distruzione degli oggetti che non sono più utilizzabili essendo sotto la camera e non più raggiungibili dal giocatore*/
public class ObjectDestruction : MonoBehaviour
{   
    //oggetto posizionato al di sotto della camera che funziona da punto di riferimento per l'eliminazione di tutti gli oggetti
    public GameObject objectDestructionPoint;
    public bool firstObject = true;
    

    // Start is called before the first frame update
    void Start()
    {
        objectDestructionPoint = GameObject.Find("ObjectDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {   
        //se l'oggetto corrente si trova al di sotto dell'objectDestructionPoint allora viene distrutto
        if (transform.position.y < objectDestructionPoint.transform.position.y)
        {            
             Destroy(gameObject);
        }
    }
}
