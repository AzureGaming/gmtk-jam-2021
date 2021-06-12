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
        projectile.GetComponent<Rigidbody2D>().AddForce(origin.right * 5, ForceMode2D.Impulse);
        GameObject line = Instantiate(lineRope);
        line.GetComponent<LineRope>().destination = projectile.transform;
    }
}
