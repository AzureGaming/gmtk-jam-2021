using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Fixable : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Hlelo");
        }
    }
}
