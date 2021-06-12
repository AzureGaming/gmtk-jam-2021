using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : MonoBehaviour {
    public Health source;
    public RepairedState repairedState;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Repair();
        }
    }

    void Repair() {
        if (source.value < 100 && source.value >= 0) {
            source.value += 1;
        }

        if (source.value == 100) {
            repairedState?.Repaired();
        }
    }
}
