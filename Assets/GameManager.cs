using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public delegate void RepairedShipPart();
    public static RepairedShipPart OnRepairedShipPart;
    public delegate void PlayerDeath();
    public static PlayerDeath OnPlayerDeath;

    int objectsToFix = 6;
    int numberOfFixedObjects;
    bool isGameOver = false;

    private void OnEnable() {
        OnRepairedShipPart += IncrementFixed;
        OnPlayerDeath += SetGameOverFlag;
    }

    private void OnDisable() {
        OnRepairedShipPart -= IncrementFixed;
        OnPlayerDeath -= SetGameOverFlag;
    }

    private void Start() {
        SetupGame();
    }

    void SetupGame() {
        numberOfFixedObjects = 0;
        StartCoroutine(GameLoop());
    }

    void SetGameOverFlag() {
        isGameOver = true;
    }

    void GameOver() {
        Debug.LogWarning("IMPLEMENT LOSE");
    }

    void WinGame() {
        Debug.LogWarning("IMPLEMENT WIN");
    }

    void IncrementFixed() {
        numberOfFixedObjects++;
    }

    IEnumerator GameLoop() {
        while (numberOfFixedObjects == objectsToFix) {
            if (isGameOver) {
                GameOver();
                yield break;
            }
            yield return null;
        }
        WinGame();
    }
}
