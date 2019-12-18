﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        if(scoreText != null) {
            int Time = PlayerPrefs.GetInt("Time");
            int bestTime = PlayerPrefs.GetInt("BestTime");

            if(Time > bestTime) {
                bestTime = Time;
                PlayerPrefs.SetInt("BestTime",bestTime);
            }

            scoreText.SetText( "Time: " + Time/60 + ":" + Time%60 + "\n"
                + "Best time: " + bestTime / 60 + ":" + bestTime % 60);


        }
    }
}