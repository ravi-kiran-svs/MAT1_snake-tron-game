using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMult : SplEatable {

    public int xValue = 2;

    public override IEnumerator OnConsume(SnakeExtras snake) {
        snake.scoreX = xValue;
        snake.snakeUI.SetScoreXText(true);

        yield return new WaitForSeconds(timeDuration);
        snake.scoreX = 1;
        snake.snakeUI.SetScoreXText(false);
    }
}
