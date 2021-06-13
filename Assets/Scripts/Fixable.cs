using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : MonoBehaviour {
    public Health source;
    public RepairedState repairedState;
    public AudioSource welding;
    public GameObject sparks;

    Collider2D collider2d;
    GameObject sparksRef;

    private void Awake() {
        collider2d = GetComponent<Collider2D>();
    }

    private void Start() {
        collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && source.value < 100) {
            welding.Play();
            sparksRef = Instantiate(sparks, transform.position, Quaternion.identity);
            IdleState.OnFixing?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Repair();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            IdleState.OnStopFixing?.Invoke();
            welding.Stop();
            if (sparksRef) {
                Destroy(sparksRef);
            }
        }
    }

    void Repair() {
        if (source.value < 100 && source.value >= 0) {
            source.value += 1;
        }

        if (source.value == 100) {
            if (sparksRef) {
                Destroy(sparksRef);
            }
            IdleState.OnStopFixing?.Invoke();
            repairedState?.Repaired();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
