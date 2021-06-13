using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBackground : MonoBehaviour {
    public GameObject backgroundPrefab;

    GameObject nextBackground;
    float speed = 0.003f;
    //float speed = 0.01f;

    private void Start() {
        StartCoroutine(SlideRoutine());
    }

    IEnumerator SlideRoutine() {
        for (; ; ) {
            backgroundPrefab.transform.position += Vector3.right * speed;

            if (nextBackground) {
                nextBackground.transform.position += Vector3.right * speed;
            }

            if (backgroundPrefab.transform.position.x >= 10 && nextBackground == null) {
                Vector3 newPos = backgroundPrefab.transform.position;
                float halfWidth = backgroundPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                newPos.x = backgroundPrefab.GetComponent<Renderer>().bounds.min.x - halfWidth;
                nextBackground = Instantiate(backgroundPrefab, newPos, Quaternion.identity, transform);
            }

            if (backgroundPrefab.transform.position.x >= 108) {
                Destroy(backgroundPrefab);
                backgroundPrefab = nextBackground;
                nextBackground = null;
            }

            yield return null;
        }
    }
}
