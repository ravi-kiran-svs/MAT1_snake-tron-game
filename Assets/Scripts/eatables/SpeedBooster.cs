using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : SplEatable {

    public float speedX = 2;

    public override IEnumerator OnConsume(SnakeExtras snake) {
        snake.speedScale = speedX;
        snake.snakeUI.SetSpeedText(true);

        yield return new WaitForSeconds(timeDuration);
        snake.speedScale = 1;
        snake.snakeUI.SetSpeedText(false);
    }

}
