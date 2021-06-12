using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Grapple();
    public static Grapple OnGrapple;

    public GameObject connectedAbove;
    public GameObject connectedBelow;
    public PlayerState state;

    void Update() {
        state = state.HandleInput();
    }
}
