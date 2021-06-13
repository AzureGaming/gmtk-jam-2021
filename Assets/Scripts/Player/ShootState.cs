using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : PlayerState {
    public GameObject hookPrefab;
    public Transform origin;
    public Sprite shoot;
    public Sprite shootWithFuel;
    public List<AudioSource> shootSounds;
    public float projectileSpeed = 4f;
    public PlayerState idle;

    GameObject currentHook;
    bool isHookActive = false;

    public override PlayerState HandleInput() {
        if (Input.GetMouseButtonUp(0) && isHookActive) {
            return idle;
        }
        OnEnterState();
        return this;
    }

    void OnEnterState() {
        if (fuel.hasFuel) {
            spriteR.sprite = shootWithFuel;
        } else {
            spriteR.sprite = shoot;
        }
    }

    private void Update() {
        Shoot();
    }

    void Shoot() {
        if (Input.GetMouseButtonDown(0) && !isHookActive) {
            StartCoroutine(ShootRoutine());
        }
    }

    IEnumerator ShootRoutine() {
        CreateHook();
        isHookActive = true;
        MoveHook();

        yield return new WaitUntil(() => !Input.GetMouseButton(0));
        currentHook.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        float distance = Vector2.Distance(currentHook.transform.position, origin.position);
        while (distance > 0.2f) {
            RetractHook();
            distance = Vector2.Distance(currentHook.transform.position, origin.position);
            yield return null;
        }
        Destroy(currentHook);
        isHookActive = false;
    }

    void CreateHook() {
        //Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        PlayShootSound();
        //currentHook.GetComponentInChildren<RopeScript>().destination = destination;
    }

    void MoveHook() {
        currentHook.GetComponent<Rigidbody2D>().velocity = origin.right * projectileSpeed;
    }

    void RetractHook() {
        currentHook.transform.LookAt(origin.position);
        currentHook.GetComponent<Rigidbody2D>().velocity = currentHook.transform.forward * 10f;
    }

    void PlayShootSound() {
        int index = Random.Range(0, shootSounds.Count);
        shootSounds[index].Play();
    }
}
