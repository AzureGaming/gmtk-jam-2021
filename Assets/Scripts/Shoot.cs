using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform origin;
    public GameObject projectilePrefab;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnProjectile();
        }
    }

    void SpawnProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, origin);
        //projectile.GetComponent<HingeJoint2D>()
    }
}
