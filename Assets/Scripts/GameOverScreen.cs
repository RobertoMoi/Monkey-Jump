using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOver;
    public Text pointsText; //punteggio raggiunto dal giocatore nella partita corrente

    public void Start()
    {   
        //Se il punteggio della partita corrente non esiste visualizza lo score a 0 altrimenti visualizza lo score fatto dal giocatore
        if (CommonVariablesBetweenScenes.scoreGameOver is null)
            pointsText.text = "Score: 0";      
        else
            pointsText.text = CommonVariablesBetweenScenes.scoreGameOver.text;
        
    }

    public void NewGame()
    {
        //Inizia immediatamente una nuova partita caricando la scena di gioco
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Home()
    {
        //Torna al menù principale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
