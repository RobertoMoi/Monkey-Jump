using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        High
    }

    public static DifficultyLevel difficultyChoice;
}
