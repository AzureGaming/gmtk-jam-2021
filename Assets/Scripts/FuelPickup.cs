using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : MonoBehaviour {
    public Sprite empty;

    bool hasFuel = true;
    SpriteRenderer spriteR;

    private void Awake() {
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Fuel comp) && hasFuel) {
            comp.Pickup();
            spriteR.sprite = empty;
            hasFuel = false;
            TaskList.OnPickUpFuel?.Invoke();
        }
    }
}
