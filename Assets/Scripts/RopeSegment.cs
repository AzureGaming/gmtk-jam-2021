using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour {
    public GameObject connectedAbove;
    public GameObject connectedBelow;

    HingeJoint2D hingeJoint2d;

    private void Awake() {
        hingeJoint2d = GetComponent<HingeJoint2D>();
    }

    private void Start() {
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
