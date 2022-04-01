using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;

    public void Pause()
    {
        //se si entra nel menù di pausa viene attiva il gameobject PauseMenu e viene fermato il gioco
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        //esce dal gioco
        Debug.Log("QUIT!");
        Application.Quit();

    }

    public void Resume()
    {
        //il menù di pausa viene disattivato e il gioco viene reimpostato alla velocità normale
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        //Ritorna al meù principale e viene reimpostata la velocità di gioco per non avere problemi alla partita successiva
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
    }


}
