using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    GameObject player;

    private void Awake() {
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update() {
        if (player) {
            Vector3 playerPos = player.transform.position;
            Vector3 newPos = transform.position;

            newPos.x = playerPos.x;
            newPos.y = playerPos.y;

            transform.position = newPos;

        }
    }
}
