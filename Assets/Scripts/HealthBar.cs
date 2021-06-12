using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public Transform bar;
    public Health data;

    private void Update() {
        float normalizedHealth = data.health / 100f;
        SetSize(normalizedHealth);
    }

    void SetSize(float sizeNormalized) {
        Vector2 scale = bar.localScale;
        scale.x = sizeNormalized;
        bar.localScale = scale;
    }
}
