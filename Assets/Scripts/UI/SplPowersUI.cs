using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SplPowersUI : MonoBehaviour {

    [SerializeField] private TMP_Text redScoreXText;
    [SerializeField] private TMP_Text redShieldText;
    [SerializeField] private TMP_Text redSpeedText;

    [SerializeField] private TMP_Text blueScoreXText;
    [SerializeField] private TMP_Text blueShieldText;
    [SerializeField] private TMP_Text blueSpeedText;

    public void SetScoreXText(int colour, bool enabled) {
        if (colour == 0) {
            redScoreXText.gameObject.SetActive(enabled);
        } else if (colour == 1) {
            blueScoreXText.gameObject.SetActive(enabled);
        }
    }

    public void SetShieldText(int colour, bool enabled) {
        if (colour == 0) {
            redShieldText.gameObject.SetActive(enabled);
        } else if (colour == 1) {
            blueShieldText.gameObject.SetActive(enabled);
        }
    }

    public void SetSpeedText(int colour, bool enabled) {
        if (colour == 0) {
            redSpeedText.gameObject.SetActive(enabled);
        } else if (colour == 1) {
            blueSpeedText.gameObject.SetActive(enabled);
        }
    }

}
