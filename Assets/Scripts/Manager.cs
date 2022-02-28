using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Manager : MonoBehaviour
{
    private int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        score++;

        scoreText.text = "Score: " + score;
    }
}
