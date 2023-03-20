using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text blueScoreText;

    private int redScore = 0;
    private int blueScore = 0;

    public void AddScore(int colour, int score) {
        if (colour == 0) {
            redScore += score;
            redScoreText.text = "score : " + redScore;

        } else if (colour == 1) {
            blueScore += score;
            blueScoreText.text = "score : " + blueScore;
        }

    }

}
