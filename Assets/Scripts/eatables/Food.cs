using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Eatable {

    public override IEnumerator OnConsume(SnakeExtras snake) {
        snake.hasFood = true;

        snake.score += snake.scoreX;
        snake.snakeUI.SetScoreText(snake.score);

        yield return new WaitForEndOfFrame();
    }

}
