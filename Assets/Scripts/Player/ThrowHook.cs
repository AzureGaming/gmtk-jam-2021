using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : PlayerState {
    public GameObject hookPrefab;
    public Transform origin;

    GameObject currentHook;
    bool isHookActive = false;

    public override PlayerState HandleInput() {
        return this;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isHookActive) {
                Destroy(currentHook);
                CreateHook();
            } else {
                CreateHook();
                isHookActive = true;
            }
        }
    }

    void CreateHook() {
        //Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        currentHook.GetComponent<Rigidbody2D>().velocity = origin.right * 2;
        //currentHook.GetComponentInChildren<RopeScript>().destination = destination;
    }
}
