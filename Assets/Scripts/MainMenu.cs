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
    const int HighScoreSlots = 10;
    public Text[] score = new Text[HighScoreSlots];

    public void PlayGame()
    {
        GameData newData = new GameData();

        PlayerPrefs.SetString("PlayerName", nickname.text);
        PlayerPrefs.Save();

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
        savePath = Application.persistentDataPath + "/scores.sav";
        BinaryFormatter formatter = new BinaryFormatter();
        GameData currentData = new GameData(); //creo i punteggi di default

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
        for (int i = 0; i < HighScoreSlots; i++)
        {
            score[i].text = currentData.highScores[i].name + ": " + currentData.highScores[i].score;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
