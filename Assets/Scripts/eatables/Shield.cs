using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SplEatable {

    public override IEnumerator OnConsume(SnakeExtras snake) {
        snake.hasShield = true;
        snake.snakeUI.SetShieldText(true);

        yield return new WaitForSeconds(timeDuration);
        snake.hasShield = false;
        snake.snakeUI.SetShieldText(false);
    }

}
