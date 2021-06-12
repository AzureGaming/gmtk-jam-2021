using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRepaired : RepairedState {
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void Repaired() {
        animator.SetTrigger("Repaired");
    }
}
