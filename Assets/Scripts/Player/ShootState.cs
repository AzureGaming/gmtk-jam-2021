using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : PlayerState {
    public GameObject hookPrefab;
    public Transform origin;
    public Sprite shoot;
    public Sprite shootWithFuel;
    public List<AudioSource> shootSounds;

    GameObject currentHook;
    bool isHookActive = false;


    public override PlayerState HandleInput() {
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
        PlayShootSound();
        //currentHook.GetComponentInChildren<RopeScript>().destination = destination;
    }

    void PlayShootSound() {
        int index = Random.Range(0, shootSounds.Count);
        shootSounds[index].Play();
    }
}
