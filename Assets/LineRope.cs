using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRope : MonoBehaviour {
    public Transform origin;
    struct RopeSegment {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos) {
            posNow = pos;
            posOld = pos;
        }
    }

    LineRenderer lineRenderer;
    List<RopeSegment> ropeSegments = new List<RopeSegment>();
    float ropeSegLen = 0.25f;
    int segmentLength = 35;
    float lineWidth = 0.1f;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start() {
        //Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 ropeStartPoint = origin.position;

        for (int i = 0; i < segmentLength; i++) {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    void Update() {
        DrawRope();
    }

    private void FixedUpdate() {
        Simulate();
    }

    private void Simulate() {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < segmentLength; i++) {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++) {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint() {
        // Constrain to origin
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = origin.position;
        ropeSegments[0] = firstSegment;

        for (int i = 0; i < segmentLength - 1; i++) {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = ( firstSeg.posNow - secondSeg.posNow ).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen) {
                changeDir = ( firstSeg.posNow - secondSeg.posNow ).normalized;
            } else if (dist < ropeSegLen) {
                changeDir = ( secondSeg.posNow - firstSeg.posNow ).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0) {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            } else {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope() {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[segmentLength];
        for (int i = 0; i < segmentLength; i++) {
            ropePositions[i] = ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }


}