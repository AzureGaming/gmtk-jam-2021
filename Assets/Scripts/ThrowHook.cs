using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour
{
    public GameObject hookPrefab;

    GameObject currentHook;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);

            currentHook.GetComponent<RopeScript>().destination = destination;
        }
    }
}
