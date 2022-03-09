using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class GameData
{   
    const int PlayerDataSlots = 10;
    public PlayerData[] highScores = new PlayerData[PlayerDataSlots];
}

[System.Serializable]
public struct PlayerData : IComparable<PlayerData>
{  
    #nullable enable
    private int? _pos;
    public int pos { get { return _pos ?? 0; } set { _pos = value; } }
    private int? _score;
    public int score { get { return _score ?? 0; } set { _score = value; } }
    private string? _name;
    public string name { get { return _name ?? "no name"; } set { _name = value; } }
    #nullable disable

    public int CompareTo(PlayerData other)
    {
        return score.CompareTo(other.score);
    }
}

public class Manager : MonoBehaviour
{
    const int HighScoreSlotsTemp = 11;
    const int HighScoreSlots = 10;
    private int score;
    public Text scoreText;
    private string savePath;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void UpdateScore(int points)
    {
        score += points;
       
        scoreText.text = "Score: " + score;
    }

    private void UpdateScores(ref GameData g, string playerName, int score)
    {
        //Salva i dati del giocatore corrente
        PlayerData newScore = new PlayerData();
        newScore.score = score;
        newScore.name = playerName;
        
        //Creazione array di supporto con tutti i punteggi più il nuovo
        PlayerData[] allScores = new PlayerData[HighScoreSlotsTemp];

        for(int i = 0; i < HighScoreSlotsTemp; i++)
        {
            if (i < HighScoreSlots)
                allScores[i] = g.highScores[i];
            else
                allScores[i] = newScore;
        }
        
        //Prima ordina l'array di supporto in maniera crescente successivamente lo inverte in modo che sia ordinato in maniera decrescente
        Array.Sort(allScores);
        Array.Reverse(allScores);
        
        //Vengono inseriti i nuovi dieci punteggi migliori, escludendo l'undicesimo
        for (int i = 0; i < HighScoreSlots; i++)
        {            
            g.highScores[i] = allScores[i];
        }
    }

    public void saveData()
    {
        savePath = Application.persistentDataPath + "/scores.sav";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Open(savePath, FileMode.Open);
        GameData currentData;
        currentData = (GameData)formatter.Deserialize(fileStream);

        fileStream.Close();
        fileStream = File.Open(savePath, FileMode.Create);
        UpdateScores(ref currentData, PlayerPrefs.GetString("PlayerName"), score);

        formatter.Serialize(fileStream, currentData);
        fileStream.Close();
    }
}
