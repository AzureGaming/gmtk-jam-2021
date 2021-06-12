using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDropoff : MonoBehaviour {
    bool isActive = true;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Fuel comp) && comp.hasFuel && isActive) {
            comp.Drop();
            animator.SetTrigger("Fueled");
            isActive = false;
        }
    }
}
