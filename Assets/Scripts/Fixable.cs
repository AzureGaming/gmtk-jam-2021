using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : MonoBehaviour {
    public Health source;
    public RepairedState repairedState;

    Collider2D collider2d;

    private void Awake() {
        collider2d = GetComponent<Collider2D>();
    }

    private void Start() {
        collider2d.enabled = true;
    }

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
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
