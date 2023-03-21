using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField] Snake redSnake;
    [SerializeField] Snake blueSnake;
    [SerializeField] UIUI ui;

    [SerializeField] ScoreUI scoreUI;
    [SerializeField] SplPowersUI splPowersUI;

    [SerializeField] GameObject Food;
    [SerializeField] GameObject ScoreMult;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject SpeedBooster;

    [SerializeField] private float tSnake = 1;
    [SerializeField] private float tFoodSpawn = 1;
    [SerializeField] private float tSplSpawn = 5;

    private Vector2[] dir = { Vector2.down, Vector2.down };

    private List<Eatable> eatables = new();

    private IEnumerator Start() {
        StartCoroutine(StartSnake(redSnake));

        // no noticable delay in the game
        //yield return new WaitForSeconds(tSnake / 2);

        StartCoroutine(StartSnake(blueSnake));

        yield return new WaitForSeconds(tFoodSpawn * 2);
        SpawnFood();

        yield return new WaitForSeconds(tSplSpawn);
        SpawnScoreMult();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            dir[0] = Vector2.down;

        } else if (Input.GetKey(KeyCode.UpArrow)) {
            dir[0] = Vector2.up;

        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            dir[0] = Vector2.left;

        } else if (Input.GetKey(KeyCode.RightArrow)) {
            dir[0] = Vector2.right;
        }

        if (Input.GetKey(KeyCode.S)) {
            dir[1] = Vector2.down;

        } else if (Input.GetKey(KeyCode.W)) {
            dir[1] = Vector2.up;

        } else if (Input.GetKey(KeyCode.A)) {
            dir[1] = Vector2.left;

        } else if (Input.GetKey(KeyCode.D)) {
            dir[1] = Vector2.right;
        }

        /*if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnFood();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            SpawnScoreMult();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            SpawnShield();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            SpawnSpeedBooster();
        }*/
    }

    private IEnumerator StartSnake(Snake snake) {
        while (true) {
            yield return new WaitForSeconds(tSnake / snake.GetSpeedScale());
            UpdateSnakePosition(snake, dir[GetSnakeAsInt(snake)]);
        }
    }

    private void UpdateSnakePosition(Snake snake, Vector2 dir) {
        // moving the snake
        snake.UpdatePosition(dir);

        Vector2 headPos = snake.GetHeadPos();

        // checking if snake eats itself
        for (int i = 1; i < snake.transform.childCount; i++) {
            if (headPos == (Vector2)snake.transform.GetChild(i).position) {
                if (snake.HasShield()) {
                    snake.ShedSkin(i);

                } else {
                    ui.GameOver(GetSnakeAsInt(GetOtherSnake(snake)));
                }
            }
        }

        // checking if snake eats the other snake
        for (int i = 0; i < GetOtherSnake(snake).transform.childCount; i++) {
            if (headPos == (Vector2)GetOtherSnake(snake).transform.GetChild(i).position) {
                if (snake.HasShield()) {
                    if (i == 0) {
                        ui.GameOver(GetSnakeAsInt(snake));

                    } else {
                        GetOtherSnake(snake).ShedSkin(i);
                    }

                } else {
                    ui.GameOver(GetSnakeAsInt(GetOtherSnake(snake)));
                }
            }
        }

        // checking if snake eats any food
        foreach (Eatable eatable in eatables) {
            if (headPos == (Vector2)eatable.transform.position) {
                eatables.Remove(eatable);
                Destroy(eatable.gameObject);

                if (eatable is Food) {
                    snake.EatFood();
                    scoreUI.AddScore(GetSnakeAsInt(snake), snake.GetScoreX());

                    StartCoroutine(OnFoodEaten());

                } else if (eatable is ScoreMult) {
                    snake.SetScoreX((eatable as ScoreMult).xValue);
                    splPowersUI.SetScoreXText(GetSnakeAsInt(snake), true);

                    StartCoroutine(ResetSnakeScoreX(snake, (eatable as ScoreMult).time));

                    StartCoroutine(OnSplEaten());

                } else if (eatable is Shield) {
                    snake.SetShield(true);
                    splPowersUI.SetShieldText(GetSnakeAsInt(snake), true);

                    StartCoroutine(ResetSnakeShield(snake, (eatable as Shield).time));

                    StartCoroutine(OnSplEaten());

                } else if (eatable is SpeedBooster) {
                    snake.SetSpeedScale((eatable as SpeedBooster).speedX);
                    splPowersUI.SetSpeedText(GetSnakeAsInt(snake), true);

                    StartCoroutine(ResetSnakeSpeed(snake, (eatable as SpeedBooster).time));

                    StartCoroutine(OnSplEaten());
                }

                break;
            }
        }
    }

    // making sure nothing is there on that square
    private void SpawnFood() {
        int xRand = Random.Range(-8, 8);
        int yRand = Random.Range(-8, 8);

        GameObject food = Instantiate(Food, new Vector2(xRand, yRand), Quaternion.identity);

        eatables.Add(food.GetComponent<Food>());
    }

    private void SpawnScoreMult() {
        int xRand = Random.Range(-8, 8);
        int yRand = Random.Range(-8, 8);

        GameObject scoreMult = Instantiate(ScoreMult, new Vector2(xRand, yRand), ScoreMult.transform.rotation);

        eatables.Add(scoreMult.GetComponent<ScoreMult>());
    }

    private void SpawnShield() {
        int xRand = Random.Range(-8, 8);
        int yRand = Random.Range(-8, 8);

        GameObject shield = Instantiate(Shield, new Vector2(xRand, yRand), Shield.transform.rotation);

        eatables.Add(shield.GetComponent<Shield>());
    }

    private void SpawnSpeedBooster() {
        int xRand = Random.Range(-8, 8);
        int yRand = Random.Range(-8, 8);

        GameObject speedBooster = Instantiate(SpeedBooster, new Vector2(xRand, yRand), Quaternion.identity);

        eatables.Add(speedBooster.GetComponent<SpeedBooster>());
    }

    private IEnumerator OnFoodEaten() {
        yield return new WaitForSeconds(tFoodSpawn);
        SpawnFood();
    }

    private IEnumerator OnSplEaten() {
        yield return new WaitForSeconds(tSplSpawn);

        int x = Random.Range(0, 3);

        if (x == 0) {
            SpawnScoreMult();

        } else if (x == 1) {
            SpawnShield();

        } else if (x == 2) {
            SpawnSpeedBooster();
        }

    }

    private Snake GetOtherSnake(Snake snake) {
        if (snake == redSnake) {
            return blueSnake;
        } else {
            return redSnake;
        }
    }

    private int GetSnakeAsInt(Snake snake) {
        if (snake == redSnake) {
            return 0;
        } else {
            return 1;
        }
    }

    private IEnumerator ResetSnakeScoreX(Snake snake, int time) {
        yield return new WaitForSeconds(time);
        snake.SetScoreX(1);
        splPowersUI.SetScoreXText(GetSnakeAsInt(snake), false);
    }

    private IEnumerator ResetSnakeShield(Snake snake, int time) {
        yield return new WaitForSeconds(time);
        snake.SetShield(false);
        splPowersUI.SetShieldText(GetSnakeAsInt(snake), false);
    }

    private IEnumerator ResetSnakeSpeed(Snake snake, int time) {
        yield return new WaitForSeconds(time);
        snake.SetSpeedScale(1);
        splPowersUI.SetSpeedText(GetSnakeAsInt(snake), false);
    }

}
