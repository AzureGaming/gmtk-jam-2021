using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState {
    public delegate void Fixing();
    public static Fixing OnFixing;
    public delegate void StopFixing();
    public static StopFixing OnStopFixing;

    public PlayerState shootState;
    public Sprite idle;
    public Sprite idleWithFuel;
    public Sprite idleWithFuelWithFixing;
    public Sprite idleWithoutFuelWithFixing;
    public AudioSource jetSound;
    public GameObject thrusterSmoke;

    bool canMove = true;
    Rigidbody2D rb;

    protected override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        OnFixing += SetFixing;
        OnStopFixing += UnSetFixing;
    }

    private void OnDisable() {
        OnFixing -= SetFixing;
        OnStopFixing -= UnSetFixing;
    }

    public override PlayerState HandleInput() {
        OnEnterState();
        if (Input.GetMouseButtonDown(0)) {
            return shootState;
        }

        return this;
    }

    void SetFixing() {
        isFixing = true;
    }

    void UnSetFixing() {
        isFixing = false;
    }


    void OnEnterState() {
        if (isFixing && fuel.hasFuel) {
            spriteR.sprite = idleWithFuelWithFixing;
        } else if (isFixing && !fuel.hasFuel) {
            spriteR.sprite = idleWithoutFuelWithFixing;
        } else if (!isFixing && fuel.hasFuel) {
            spriteR.sprite = idleWithFuel;
        } else {
            spriteR.sprite = idle;
        }
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            Move(Vector2.right);
        } else if (Input.GetKey(KeyCode.W)) {
            Move(Vector2.up);
        } else if (Input.GetKey(KeyCode.S)) {
            Move(Vector2.down);
        } else if (Input.GetKey(KeyCode.A)) {
            Move(Vector2.left);
        }
    }

    void Move(Vector2 direction) {
        if (canMove) {
            float speed = 2f;
            Vector2 velocity = direction * speed;
            rb.AddForce(velocity, ForceMode2D.Impulse);
            StartCoroutine(MoveCooldown());
        }
    }

    IEnumerator MoveCooldown() {
        canMove = false;
        yield return new WaitForSeconds(3f);
        canMove = true;
    }
}
