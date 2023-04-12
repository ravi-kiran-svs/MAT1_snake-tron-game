using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeUnityEvent : UnityEvent<SnakeMovement> { }

public class SnakeMovement : MonoBehaviour {

    public SnakeUnityEvent OnDead = new SnakeUnityEvent();

    [SerializeField] private float tSnake = 1;
    [SerializeField] private KeyCode[] directionKeys = { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };

    private GameMaster gameMaster;

    private Vector2 direction = Vector2.down;

    [HideInInspector] public bool hasFood = false;
    [HideInInspector] public int scoreX = 1;
    [HideInInspector] public bool hasShield = false;
    [HideInInspector] public float speedScale = 1;

    private void Awake() {
        gameMaster = transform.parent.GetComponent<GameMaster>();
    }

    public IEnumerator StartSnake() {
        while (true) {
            yield return new WaitForSeconds(tSnake / speedScale);

            UpdatePosition();

            SnakeMovement snakeToCollide = gameMaster.GetAnySnakeBodyAt(transform.GetChild(0).position, this);
            if (snakeToCollide) {
                OnDead.Invoke(this);
            }
        }
    }

    private void UpdatePosition() {
        if (transform.childCount > 1) {
            if (GetPosAfterMove(transform.GetChild(0).position, direction) == (Vector2)transform.GetChild(1).position) {
                direction = -direction;
            }
        }

        Vector2 toPos = GetPosAfterMove(transform.GetChild(0).position, direction);

        for (int i = 0; i < transform.childCount; i++) {
            Vector2 p = transform.GetChild(i).position;
            transform.GetChild(i).position = toPos;
            toPos = p;
        }

        /*if (hasFood) {
            GameObject snakeBody = Instantiate(SnakeBody, toPos, Quaternion.identity, transform);

            hasFood = false;
        }*/
    }

    private void Update() {
        if (Input.GetKey(directionKeys[0])) {
            direction = Vector2.up;

        } else if (Input.GetKey(directionKeys[1])) {
            direction = Vector2.right;

        } else if (Input.GetKey(directionKeys[2])) {
            direction = Vector2.down;

        } else if (Input.GetKey(directionKeys[3])) {
            direction = Vector2.left;
        }
    }

    private Vector2 GetPosAfterMove(Vector2 p, Vector2 dir) {
        Vector2 board = gameMaster.GetBoardDimensions();
        p += dir;

        // wrapping around code
        if (p.x == board.x / 2) {
            p.x = -board.x / 2;
        } else if (p.x == (-board.x / 2) - 1) {
            p.x = (board.x / 2) - 1;
        } else if (p.y == board.y / 2) {
            p.y = -board.y / 2;
        } else if (p.y == (-board.y / 2) - 1) {
            p.y = (board.y / 2) - 1;
        }

        return p;
    }

}
