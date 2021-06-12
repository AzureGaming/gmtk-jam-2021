using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {
    public Vector2 destination;
    public float speed = 1f;
     float distance = 2f;
    public GameObject nodePrefab;

    GameObject player;
    GameObject lastNode;
    bool isDone = false;

    private void Start() {
        player = GameObject.Find("Player");
        lastNode = gameObject;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        if ((Vector2)transform.position != destination) {
            // Hook is travelling
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance) {
                CreateNode();
            }
        } else if (!isDone) {
            isDone = true;
            Debug.Log("done");
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }


    }

    void CreateNode() {
        Vector2 pos = player.transform.position - lastNode.transform.position;
        pos.Normalize();
        pos *= distance;
        pos += (Vector2)lastNode.transform.position;

        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity);

        node.transform.parent = transform;

        lastNode.GetComponent<HingeJoint2D>().connectedBody = node.GetComponent<Rigidbody2D>();
        lastNode = node;
    }
}
