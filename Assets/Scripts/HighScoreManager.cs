using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    private string savePath;
    const int scoreSlot = 10;
    public Text[] score = new Text[scoreSlot];

    void Start()
    {
        savePath = Application.persistentDataPath + "/scores.sav";
        BinaryFormatter formatter = new BinaryFormatter();
        GameData currentData = new GameData(); //creo i punteggi di default

        //TODO: carica i punteggi in currentdata se il file savePath esite, altrimenti salva i punteggi di default su file
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
        for(int i = 0; i < scoreSlot; i++)
        {
            score[i].text = currentData.highScores[i].name + ": " + currentData.highScores[i].score;
        }
    }
}
