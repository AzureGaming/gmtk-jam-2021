using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAndPersist : RepairedState {
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void Repaired() {
        animator.SetTrigger("Repaired");
    }
}