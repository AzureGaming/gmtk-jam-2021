using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public Rigidbody2D hook;
    public GameObject[] ropeSegmentPrefabs;
    public GameObject player;
    public int numLinks = 5;

    private void Start() {
        Debug.Log("Rope start");
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
        GameObject playerInstance = GameObject.Find("Player");
        playerInstance.transform.parent = transform;
        playerInstance.transform.position = transform.position;
        playerInstance.GetComponent<HingeJoint2D>().connectedBody = prevBod;
        Player.OnGrapple?.Invoke();

    }

    void CreateRopeSegment() {
    }
}
