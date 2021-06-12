using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRope : MonoBehaviour {
    public Transform origin;
    public Transform destination;
    struct RopeSegment {
        public Vector2 currentPos;
        public Vector2 oldPos;

        public RopeSegment(Vector2 pos) {
            currentPos = pos;
            oldPos = pos;
        }
    }

    LineRenderer lineRenderer;
    List<RopeSegment> ropeSegments = new List<RopeSegment>();
    float ropeSegLen = 0.05f;
    int segmentLength = 35;
    int targetSegmentLength = 35;
    float lineWidth = 0.1f;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    void Start() {
        if (!origin) {
            origin = GameObject.Find("Gun").transform;
        }

        if (!destination) {
            destination = GameObject.Find("RopeDestination(Clone)").transform;
        }

        Vector3 ropeStartPoint = origin.position;
        for (int i = 0; i < segmentLength; i++) {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }

        StartCoroutine(DrawRope());
    }

    private void FixedUpdate() {
        Simulate();
    }

    private void Simulate() {
        // SIMULATION
        //Vector2 forceGravity = new Vector2(5f, 0f);
        //Vector2 forceGravity = Vector2.right;
        Vector2 forceGravity = Vector2.zero;

        for (int i = 1; i < segmentLength; i++) {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.currentPos - firstSegment.oldPos;
            firstSegment.oldPos = firstSegment.currentPos;
            firstSegment.currentPos += velocity;
            firstSegment.currentPos += forceGravity * Time.fixedDeltaTime;
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
        firstSegment.currentPos = origin.position;
        ropeSegments[0] = firstSegment;

        RopeSegment lastSegment = ropeSegments[ropeSegments.Count - 1];
        lastSegment.currentPos = destination.position;
        ropeSegments[ropeSegments.Count - 1] = lastSegment;

        // Calculate distance between segments
        for (int i = 0; i < segmentLength - 1; i++) {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = ( firstSeg.currentPos - secondSeg.currentPos ).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen) {
                changeDir = ( firstSeg.currentPos - secondSeg.currentPos ).normalized;
            } else if (dist < ropeSegLen) {
                changeDir = ( secondSeg.currentPos - firstSeg.currentPos ).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0) {
                firstSeg.currentPos -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.currentPos += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            } else {
                secondSeg.currentPos += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    IEnumerator DrawRope() {
        for (; ; ) {
            Vector3[] ropePositions = new Vector3[segmentLength];

            for (int i = 0; i < segmentLength; i++) {
                ropePositions[i] = ropeSegments[i].currentPos;
            }

            lineRenderer.positionCount = ropePositions.Length;
            lineRenderer.SetPositions(ropePositions);
            yield return null;
        }
    }
}