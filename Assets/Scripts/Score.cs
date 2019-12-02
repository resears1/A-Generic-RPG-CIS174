using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int currentScore;
    public Text display;

    public static int score = 0;

    void Start()
    {
        display.text = "Score: 0";
    }

    void Update()
    {
        display.text = "Score: " + score;
    }
}
