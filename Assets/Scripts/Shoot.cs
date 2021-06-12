using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform origin;
    public GameObject projectilePrefab;
    public GameObject lineRope;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SpawnProjectile();
        }
    }

    void SpawnProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
        StartCoroutine(Test());
        //GameObject line = Instantiate(lineRope);
        //line.GetComponent<LineRope>().destination = projectile.transform;
    }

    IEnumerator Test() {
        Vector3 direction = origin.right;
        for (; ; ) {
            Vector3 pos = GameObject.Find("Hook").transform.position;
            pos += direction * 0.01f;
            GameObject.Find("Hook").transform.position = pos;
            yield return null;
        }
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, origin.right);
    }
}
