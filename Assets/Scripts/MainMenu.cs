using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField nickname;
    private string savePath;
    const int HighScoreSlots = 10; //numero di punteggi contenuti nella sezione top score
    public Text[] score = new Text[HighScoreSlots];
    
    public void PlayGame()
    {
        GameData newData = new GameData(); //creazione di un nuovo contenitore di dati

        //viene impostato e salvato il nickname del giocatore nelle PlayerPerfs
        PlayerPrefs.SetString("PlayerName", nickname.text);
        PlayerPrefs.Save();
        
        //se il livello di difficoltà è impostato a NotSet la partita non inizia altrimenti viene caricata una nuova partita
        if (CommonVariablesBetweenScenes.difficultyChoice == 0)
            return;
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AwardsGame()
    {
        Debug.Log("AWARDS!");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SetDifficulty(int difficultyIndex)
    {
        
        CommonVariablesBetweenScenes.difficultyChoice = (CommonVariablesBetweenScenes.DifficultyLevel) difficultyIndex;
        Debug.Log(CommonVariablesBetweenScenes.difficultyChoice);
    }

    // Start is called before the first frame update
    void Start()
    {
        /* ogni volta che si torna al menù principale la difficoltà viene settata di default a NotSet 
         * per evitare di prendere difficoltà impostate precedentemente */
        CommonVariablesBetweenScenes.difficultyChoice = CommonVariablesBetweenScenes.DifficultyLevel.NotSet;

        savePath = Application.persistentDataPath + "/scores.sav"; //percorso che contiene il salvataggio dei dati
        BinaryFormatter formatter = new BinaryFormatter();
        GameData currentData = new GameData(); //conterrà i dati correnti

        //se il file esiste già nel percorso di salvataggio allora verrà aperto quello altrimenti ne verrà creato uno nuovo
        if (File.Exists(savePath))
        {
            FileStream file = File.Open(savePath, FileMode.Open);
            currentData = (GameData)formatter.Deserialize(file);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(savePath, FileMode.Create);
            formatter.Serialize(file, currentData);
            file.Close();
        }

        //vengono caricati i top score attuali
        for (int i = 0; i < HighScoreSlots; i++)
        {
            score[i].text = currentData.highScores[i].name + "  " + currentData.highScores[i].score;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
