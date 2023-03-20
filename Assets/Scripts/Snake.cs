using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    [SerializeField] private GameObject SnakeBody;

    private bool hasFood = false;
    private int scoreX = 1;
    private bool hasShield = false;
    private float speedScale = 1;

    public void UpdatePosition(Vector2 direction) {
        // donot allow reverse movement
        if (transform.childCount > 1) {
            if (GetPosAfterMove(transform.GetChild(0).position, direction) == (Vector2)transform.GetChild(1).position) {
                direction = -direction;
            }
        }

        // where the head should go
        Vector2 toPos = GetPosAfterMove(transform.GetChild(0).position, direction);

        // iterate through every body part and move them
        for (int i = 0; i < transform.childCount; i++) {
            Vector2 p = transform.GetChild(i).position;
            transform.GetChild(i).position = toPos;
            toPos = p;
        }

        // if it has food = new body part
        if (hasFood) {
            GameObject snakeBody = Instantiate(SnakeBody, toPos, Quaternion.identity, transform);

            hasFood = false;
        }
    }

    public void EatFood() { hasFood = true; }

    public int GetScoreX() { return scoreX; }
    public void SetScoreX(int x) { scoreX = x; }

    public bool HasShield() { return hasShield; }
    public void SetShield(bool s) { hasShield = s; }

    public float GetSpeedScale() { return speedScale; }
    public void SetSpeedScale(float s) { speedScale = s; }

    public void ShedSkin(int i) {
        for (int ii = transform.childCount - 1; ii >= i; ii--) {
            Destroy(transform.GetChild(ii).gameObject);
        }
    }

    public Vector2 GetHeadPos() {
        return transform.GetChild(0).position;
    }

    private Vector2 GetPosAfterMove(Vector2 p, Vector2 dir) {
        p += dir;

        // wrapping around code
        if (p.x == 8) {
            p.x = -8;
        } else if (p.x == -9) {
            p.x = 7;
        } else if (p.y == 8) {
            p.y = -8;
        } else if (p.y == -9) {
            p.y = 7;
        }

        return p;
    }

}
