using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SplEatable : Eatable {

    protected int timeDuration = 6;

    public abstract override IEnumerator OnConsume(SnakeExtras snake);
}
