using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Grapple();
    public static Grapple OnGrapple;

    public GameObject connectedAbove;
    public GameObject connectedBelow;

    Rigidbody2D rb;
    HingeJoint2D hingeJoint2d;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        hingeJoint2d = GetComponent<HingeJoint2D>();
    }

    private void OnEnable() {
        OnGrapple += EnableHinge;
    }

    private void OnDisable() {
        OnGrapple -= EnableHinge;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            Vector3 newPos = transform.position;
            newPos.x += 10f;
            rb.AddForce(newPos);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            Vector3 newPos = transform.position;
            newPos.y += 10f;
            rb.AddForce(newPos);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Vector3 newPos = transform.position;
            newPos.y -= 10f;
            rb.AddForce(newPos);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            Vector3 newPos = transform.position;
            newPos.x -= 10f;
            rb.AddForce(newPos);
        }
    }

    void EnableHinge() {
        Debug.Log("Player event");
        hingeJoint2d.enabled = true;
        connectedAbove = hingeJoint2d.connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

        if (aboveSegment == null) {
            // This is the top of the rope
            hingeJoint2d.connectedAnchor = new Vector2(0, 0);
        } else {
            aboveSegment.connectedBelow = gameObject;
            // put anchor at the bottom of the sprite above us
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            hingeJoint2d.connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
    }
}
