using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour {
    public int value = 100;
    public AudioSource warning;

    bool isWarningCheckValid = true;

    void Start() {
        StartCoroutine(Deplete());
    }

    private void Update() {
        CheckOxygenThreshold();
    }

    public void Replenish() {
        isWarningCheckValid = true;
        value = 100;
    }

    IEnumerator Deplete() {
        while (value > 0) {
            value -= 2;
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.OnPlayerDeath?.Invoke();
    }

    void CheckOxygenThreshold() {
        if (value <= 50 && isWarningCheckValid) {
            warning.Play();
            isWarningCheckValid = false;
        }
    }
}
