using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeExtras : MonoBehaviour {

    [SerializeField] public int score = 0;

    [HideInInspector] public bool hasFood = false;
    [HideInInspector] public int scoreX = 1;
    [HideInInspector] public bool hasShield = false;
    [HideInInspector] public float speedScale = 1;

    [SerializeField] public SnakeUI snakeUI;

}
