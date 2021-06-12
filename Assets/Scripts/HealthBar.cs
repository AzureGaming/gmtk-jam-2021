using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public Transform bar;
    public Health data;
    public SpriteRenderer spriteR;

    private void Update() {
        float normalizedHealth = data.value / 100f;

        SetSize(normalizedHealth);
        SetColour(normalizedHealth);
    }

    void SetSize(float sizeNormalized) {
        Vector2 scale = bar.localScale;
        scale.x = sizeNormalized;
        bar.localScale = scale;
    }

    void SetColour(float sizeNormalized) {
        if (sizeNormalized > 0.75f) {
            spriteR.color = Color.green;
        } else if (sizeNormalized > 0.25f && sizeNormalized < 0.75f) {
            spriteR.color = Color.yellow;
        } else if (sizeNormalized <0.25f) {
            spriteR.color = Color.red;
        }
    }
}
