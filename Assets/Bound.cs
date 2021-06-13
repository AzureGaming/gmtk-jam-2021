using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameManager.OnPlayerDeath?.Invoke();
            GameObject.Find("Main Camera").GetComponent<FollowPlayer>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
