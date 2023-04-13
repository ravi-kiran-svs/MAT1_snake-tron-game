using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeUnityEvent : UnityEvent<SnakeMovement> { }

public class SnakeMovement : MonoBehaviour {

    public SnakeUnityEvent OnDead = new SnakeUnityEvent();

    [SerializeField] private GameObject SnakeBody;

    [SerializeField] private float tSnake = 1;
    [SerializeField] private KeyCode[] directionKeys = { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };

    private GameMaster gameMaster;
    private EatableSpawner eatablesManager;
    private SnakeExtras snakeExtras;

    private Vector2 direction = Vector2.down;

    private void Awake() {
        gameMaster = transform.parent.GetComponent<GameMaster>();
        eatablesManager = transform.parent.GetComponent<EatableSpawner>();
        snakeExtras = GetComponent<SnakeExtras>();
    }

    public IEnumerator StartSnake() {
        while (true) {
            yield return new WaitForSeconds(tSnake / snakeExtras.speedScale);

            UpdatePosition();

            CheckCollisionWithSnakes();

            CheckCollisionWithEatables();
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

        if (snakeExtras.hasFood) {
            Instantiate(SnakeBody, toPos, Quaternion.identity, transform);

            snakeExtras.hasFood = false;
        }
    }

    private void CheckCollisionWithSnakes() {
        SnakeMovement snakeToCollide = gameMaster.GetAnySnakeBodyAt(transform.GetChild(0).position, this);
        if (snakeToCollide) {
            if (snakeExtras.hasShield) {
                if (snakeToCollide == this) {
                    ShedSkin(transform.GetChild(0).position);

                } else {
                    if (transform.GetChild(0).position == snakeToCollide.transform.GetChild(0).position) {
                        snakeToCollide.OnDead.Invoke(snakeToCollide);

                    } else {
                        snakeToCollide.ShedSkin(transform.GetChild(0).position);
                    }
                }

            } else {
                OnDead.Invoke(this);
            }
        }
    }

    private void CheckCollisionWithEatables() {
        Eatable eatable = eatablesManager.GetAnyEatableAt(transform.GetChild(0).position);
        if (eatable) {
            StartCoroutine(eatable.OnConsume(snakeExtras));
            Destroy(eatable.gameObject);
        }
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

    private void ShedSkin(Vector2 p) {
        bool deleteFromNow = false;

        for (int i = 1; i < transform.childCount; i++) {
            if (!deleteFromNow) {
                if ((Vector2)transform.GetChild(i).position == p) {
                    deleteFromNow = true;
                    Destroy(transform.GetChild(i).gameObject);
                }

            } else {
                Destroy(transform.GetChild(i).gameObject);
            }
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
