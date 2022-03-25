using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Contiene variabili statiche utilizzate tra pi� scene
public class CommonVariablesBetweenScenes : MonoBehaviour
{
    //tre difficolt� pi� notset che non permette al giocatore di inziare la partita senza aver prima impostato la difficolt�
    public enum DifficultyLevel
    {
        NotSet,
        Easy,
        Medium,
        High
    }

    public static DifficultyLevel difficultyChoice;

    //Permette di passare il punteggio ottenuto dal giocatore tra la scena del gioco e quella finale di game over
    public static Text scoreGameOver;
    
}
