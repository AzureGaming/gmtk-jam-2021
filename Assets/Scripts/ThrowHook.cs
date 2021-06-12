using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {
    public GameObject hookPrefab;

    GameObject currentHook;
    bool isHookActive = false;

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
        Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);

        currentHook.GetComponent<RopeScript>().destination = destination;

    }
}
