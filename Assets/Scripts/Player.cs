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
