using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherHook : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Trigger" + collision.name);
        if (collision.CompareTag("RopeSegment")) {
            GameObject hook = GameObject.Find("Hook");
            hook.transform.parent = transform;
            hook.transform.position = transform.position;
            hook.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
    }
}
