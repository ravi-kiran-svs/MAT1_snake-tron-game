using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayMenus : MonoBehaviour {

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject redWinsText;
    [SerializeField] private GameObject blueWinsText;

    private bool isPaused = false;
    private bool isGameOver = false;

    private void Update() {
        if (!isGameOver) {
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Escape)) {
                if (!isPaused) {
                    Pause();
                } else {
                    Unpause();
                }
            }
        }
    }

    public void Pause() {
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause() {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver(int winner) {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

        if (winner == 0) {
            redWinsText.SetActive(true);
            blueWinsText.SetActive(false);
        } else {
            redWinsText.SetActive(false);
            blueWinsText.SetActive(true);
        }
    }

    public void Replay() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
