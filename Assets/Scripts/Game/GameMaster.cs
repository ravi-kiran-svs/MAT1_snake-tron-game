using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    [SerializeField] private Vector2 boardDimensions = new Vector2(16, 16);

    [SerializeField] private SnakeMovement[] snakes;
    [SerializeField] private OverlayMenus overlayMenus;

    private void Awake() {
        foreach (SnakeMovement snake in snakes) {
            snake.OnDead.AddListener(OnSnakeDead);
        }
    }

    private void Start() {
        foreach (SnakeMovement snake in snakes) {
            StartCoroutine(snake.StartSnake());
        }
    }

    private void OnSnakeDead(SnakeMovement snake) {
        overlayMenus.GameOver(1 - Array.IndexOf(snakes, snake));
    }

    public SnakeMovement GetAnySnakeBodyAt(Vector2 p, SnakeMovement originalSnake) {
        foreach (SnakeMovement snake in snakes) {

            for (int i = 0; i < snake.transform.childCount; i++) {
                if (!(snake == originalSnake && i == 0)) {
                    if (p == (Vector2)snake.transform.GetChild(i).position) {
                        return snake;
                    }
                }
            }
        }

        return null;
    }

    public Vector2 GetBoardDimensions() { return boardDimensions; }

    public SnakeMovement[] GetSnakes() { return snakes; }

}
