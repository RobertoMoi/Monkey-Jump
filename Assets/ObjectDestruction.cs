using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    public GameObject objectDestructionPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < objectDestructionPoint.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
