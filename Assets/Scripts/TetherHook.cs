using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherHook : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Hook")) {
            GameObject hook = collision.gameObject;
            hook.transform.parent = transform;
            hook.transform.position = transform.position;
            hook.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            hook.transform.SetParent(transform, false);
            //hook.transform.position = transform.position;
            hook.GetComponent<Hook>().Attach();
            hook.GetComponentInChildren<Rope>().firstNode.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
    }
}
