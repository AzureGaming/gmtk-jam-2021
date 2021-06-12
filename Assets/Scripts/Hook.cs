using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    private void Update() {
        GetComponentInChildren<Rope>().destination = transform.position;
    }

    public void Attach() {
        GetComponentInChildren<Rope>().isAttached = true;
    }
}
