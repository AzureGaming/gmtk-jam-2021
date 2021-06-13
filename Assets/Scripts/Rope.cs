using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public Vector2 destination;
    public GameObject nodePrefab;

    float speed = 20f;
    float distanceBetweenNodes = 0.01f;
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
        player = FindObjectOfType<Player>().gameObject;
        firstNode = gameObject;
        lastNode = gameObject;

        nodes.Add(lastNode);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
        float distanceBetween = Vector2.Distance(lastNode.transform.position, player.transform.position);
        bool isNodeValid = distanceBetween > distanceBetweenNodes;

        if (!isAttached && isNodeValid) {
            CreateNode();
            // Hook is travelling
            //if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distanceBetweenNodes) {
            //}
        } else if (isAttached) {
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            //if (Input.GetMouseButton(1)) {
            //    Debug.Log(nodes.Count);
            //    GameObject secondLastNode = nodes[nodes.Count - 2];
            //    secondLastNode.GetComponent<HingeJoint2D>().connectedBody = lastNode.GetComponent<HingeJoint2D>().connectedBody;
            //    GameObject lastNodeRef = lastNode;
            //    lastNode = secondLastNode;
            //    Destroy(lastNodeRef);
            //}
        }
        RenderLine();
    }

    public void Retrace() {
        vertexCount = 0;
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
        if (lr.positionCount > 0) {
            for (int i = 0; i < nodes.Count; i++) {
                lr.SetPosition(i, nodes[i].transform.position);
            }
            //account for player
            lr.SetPosition(nodes.Count, player.transform.position);
        }
    }


}
