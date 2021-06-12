using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBreachRepaired : RepairedState {
    public override void Repaired() {
        Destroy(gameObject);
    }
}
