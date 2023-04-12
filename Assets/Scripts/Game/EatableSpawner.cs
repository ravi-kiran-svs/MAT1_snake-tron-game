using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableSpawner : MonoBehaviour {

    private GameMaster master;

    [SerializeField] GameObject Food;
    [SerializeField] GameObject ScoreMult;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject SpeedBooster;

    private List<Eatable> eatables = new();

    private void Awake() {
        master = GetComponent<GameMaster>();
    }

    //delete later
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
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
        }
    }

    public Eatable GetAnyEatableAt(Vector2 p) {
        foreach (Eatable eatable in eatables) {
            if (p == (Vector2)eatable.transform.position) {
                eatables.Remove(eatable);
                return eatable;
            }
        }
        return null;
    }

    private void SpawnFood() {
        Vector2 spawn = GetRandomSpawnPosition();

        GameObject food = Instantiate(Food, spawn, Quaternion.identity);

        eatables.Add(food.GetComponent<Food>());
    }

    private void SpawnScoreMult() {
        Vector2 spawn = GetRandomSpawnPosition();

        GameObject scoreMult = Instantiate(ScoreMult, spawn, ScoreMult.transform.rotation);

        eatables.Add(scoreMult.GetComponent<ScoreMult>());
    }

    private void SpawnShield() {
        Vector2 spawn = GetRandomSpawnPosition();

        GameObject shield = Instantiate(Shield, spawn, Shield.transform.rotation);

        eatables.Add(shield.GetComponent<Shield>());
    }

    private void SpawnSpeedBooster() {
        Vector2 spawn = GetRandomSpawnPosition();

        GameObject speedBooster = Instantiate(SpeedBooster, spawn, Quaternion.identity);

        eatables.Add(speedBooster.GetComponent<SpeedBooster>());
    }

    private Vector2 GetRandomSpawnPosition() {
        int num_occupants = 0;
        foreach (SnakeMovement snake in master.GetSnakes()) {
            num_occupants += snake.transform.childCount;
        }
        num_occupants += eatables.Count;
        int num_avail_squares = 128 - num_occupants;

        int spawn_pos = Random.Range(0, num_avail_squares);

        int pos = 0;
        while (spawn_pos >= 0) {
            Vector3 posVec = new Vector2(pos % 16, pos / 16);
            bool isEmpty = true;

            for (int i = 0; i < master.GetSnakes()[0].transform.childCount; i++) {
                if (master.GetSnakes()[0].transform.GetChild(0).position == posVec) {
                    isEmpty = false;
                }
            }
            if (isEmpty) {
                for (int i = 0; i < master.GetSnakes()[1].transform.childCount; i++) {
                    if (master.GetSnakes()[1].transform.GetChild(0).position == posVec) {
                        isEmpty = false;
                    }
                }
            }
            if (isEmpty) {
                for (int i = 0; i < eatables.Count; i++) {
                    if (eatables[i].transform.position == posVec) {
                        isEmpty = false;
                    }
                }
            }

            if (isEmpty) {
                spawn_pos--;
            }
            if (spawn_pos >= 0) {
                pos++;
            }
        }

        return new Vector2(-8 + (pos % 16), -8 + (pos / 16));
    }
}
