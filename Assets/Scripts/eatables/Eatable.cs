using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Eatable : MonoBehaviour {
    public abstract IEnumerator OnConsume(SnakeExtras snake);
}
