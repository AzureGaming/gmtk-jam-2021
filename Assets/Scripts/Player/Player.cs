using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerState {
    public delegate void OxygenDepleted();
    public static OxygenDepleted OnOxygenDepleted;

    public GameObject connectedAbove;
    public GameObject connectedBelow;
    public PlayerState state;

    public AudioSource breathing;
    public AudioSource choking;

    private void OnEnable() {
        OnOxygenDepleted += DeathFromOxygen;
    }

    private void OnDisable() {
        OnOxygenDepleted -= DeathFromOxygen;
    }

    void Update() {
        state = state.HandleInput();
    }

    private void Start() {
        breathing.Play();
    }

    void DeathFromOxygen() {
        breathing.Stop();
        choking.Play();
    }
}
