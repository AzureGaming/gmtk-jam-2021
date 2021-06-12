using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishOxygen : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Oxygen comp)) {
            comp.Replenish();
        }
    }
}
