using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : MonoBehaviour {
    public Health source;
    public RepairedState repairedState;
    public AudioSource welding;

    Collider2D collider2d;

    private void Awake() {
        collider2d = GetComponent<Collider2D>();
    }

    private void Start() {
        collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            welding.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Repair();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            welding.Stop();
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
