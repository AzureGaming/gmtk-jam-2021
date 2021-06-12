using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState {
    public PlayerState shootState;
    public Sprite idle;
    public Sprite idleWithFuel;

    Rigidbody2D rb;

    protected override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override PlayerState HandleInput() {
        OnEnterState();
        if (Input.GetMouseButtonDown(0)) {
            return shootState;
        }

        return this;
    }

    void OnEnterState() {
        if (fuel.hasFuel) {
            spriteR.sprite = idleWithFuel;
        } else {
            spriteR.sprite = idle;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            Vector2 velocity = Vector2.zero;
            velocity = Vector2.right;
            rb.AddForce(velocity * 3f);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            Vector2 velocity = Vector2.zero;
            velocity = Vector2.up;
            rb.AddForce(velocity * 3f);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Vector2 velocity = Vector2.zero;
            velocity = Vector2.down;
            rb.AddForce(velocity * 3f);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            Vector2 velocity = Vector2.zero;
            velocity = Vector2.left;
            rb.AddForce(velocity * 3f);
        }
    }
}
