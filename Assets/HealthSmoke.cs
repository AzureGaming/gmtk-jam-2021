using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSmoke : MonoBehaviour {
    public Health source;
    public GameObject smokePrefab;

    GameObject smokeRef;

    void Update() {
        if (source.value >= 0 && source.value <= 50 && !smokeRef) {
            smokeRef = Instantiate(smokePrefab, transform, false);
        }

        if (source.value > 50 && smokeRef) {
            Destroy(smokeRef);
        }
    }
}
