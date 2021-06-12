using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {
    public Vector2 destination;
    public float speed = 1f;
    public GameObject nodePrefab;

    float distanceBetweenNodes = 0.5f;
    GameObject player;
    public GameObject firstNode;
    GameObject lastNode;
    bool isDone = false;
    public bool isAttached = false;
    List<GameObject> nodes = new List<GameObject>();
    int vertexCount = 2; // hook and player
    LineRenderer lr;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    private void Start() {
        player = GameObject.Find("Player");
        firstNode = gameObject;
        lastNode = gameObject;

        nodes.Add(lastNode);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        if ((Vector2)transform.position != destination && !isAttached) {
            // Hook is travelling
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distanceBetweenNodes) {
                CreateNode();
            }
        }
        if (isAttached) {
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }
        //else if (!isDone) {
        //    isDone = true;
        //    // if hook gets to position before all nodes are created, create the rest
        //    while (Vector2.Distance(player.transform.position, lastNode.transform.position) > distanceBetweenNodes) {
        //        CreateNode();
        //    }
        //    lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        //}

        RenderLine();
    }

    void CreateNode() {
        Vector2 pos = player.transform.position - lastNode.transform.position;
        pos.Normalize();
        pos *= distanceBetweenNodes;
        pos += (Vector2)lastNode.transform.position;

        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity);

        node.transform.parent = transform;

        lastNode.GetComponent<HingeJoint2D>().connectedBody = node.GetComponent<Rigidbody2D>();
        lastNode = node;

        nodes.Add(lastNode);
        vertexCount++; // account for new node
    }

    void RenderLine() {
        lr.positionCount = vertexCount;
        for (int i = 0; i < nodes.Count; i++) {
            lr.SetPosition(i, nodes[i].transform.position);
        }
        //account for player
        lr.SetPosition(nodes.Count, player.transform.position);
    }
}
