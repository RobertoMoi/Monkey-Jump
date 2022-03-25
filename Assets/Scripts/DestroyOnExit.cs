using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quando l'animazione finisce l'oggetto viene distrutto
public class DestroyOnExit : StateMachineBehaviour
{
    //Verifica quando inizia l'animazione e prende informazioni su di essa attraverso l'AnimatorStateInfo
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Oltre al gameobject dell'animator prende anche un parametro che contiene la durata dell'animazione
        Destroy(animator.gameObject, stateInfo.length);
    }
}
