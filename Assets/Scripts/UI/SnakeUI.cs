using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeUI : MonoBehaviour {

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text scoreXText;
    [SerializeField] private TMP_Text shieldText;
    [SerializeField] private TMP_Text speedText;

    public void SetScoreText(int score) {
        scoreText.text = "score : " + score;
    }

    public void SetScoreXText(bool enabled) {
        scoreXText.gameObject.SetActive(enabled);
    }

    public void SetShieldText(bool enabled) {
        shieldText.gameObject.SetActive(enabled);
    }

    public void SetSpeedText(bool enabled) {
        speedText.gameObject.SetActive(enabled);
    }
}
