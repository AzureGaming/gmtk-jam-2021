using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            Vector3 newPos = transform.position;
            newPos.x += 0.01f;
            transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.W)) {
            Vector3 newPos = transform.position;
            newPos.y += 0.01f;
            transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.S)) {
            Vector3 newPos = transform.position;
            newPos.y -= 0.01f;
            transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.A)) {
            Vector3 newPos = transform.position;
            newPos.x -= 0.01f;
            transform.position = newPos;
        }
    }
}
