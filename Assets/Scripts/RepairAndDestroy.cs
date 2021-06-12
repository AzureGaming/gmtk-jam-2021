using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAndDestroy : RepairedState {
    public override void Repaired() {
        GameManager.OnRepairedShipPart?.Invoke();
        Destroy(gameObject);
    }
}
