using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    private void Update() {
        GetComponentInChildren<RopeScript>().destination = transform.position;
    }

    public void Attach() {
        GetComponentInChildren<RopeScript>().isAttached = true;
    }
}
