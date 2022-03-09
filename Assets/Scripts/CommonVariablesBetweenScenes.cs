using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonVariablesBetweenScenes : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        High
    }

    public static DifficultyLevel difficultyChoice;
}
