using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public Rigidbody2D hook;
    public GameObject[] ropeSegmentPrefabs;
    public int numLinks = 1;

    private void Start() {
        GenerateRope();
    }

    void GenerateRope() {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < numLinks; i++) {
            //CreateRopeSegment();
            int index = Random.Range(0, ropeSegmentPrefabs.Length);
            GameObject newSeg = Instantiate(ropeSegmentPrefabs[index]);

            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            newSeg.GetComponent<HingeJoint2D>().connectedBody = prevBod;
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }

    void CreateRopeSegment() {
    }
}
