using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour {
    protected SpriteRenderer spriteR;
    protected Fuel fuel;
    protected bool isFixing;

    protected virtual void Awake() {
        fuel = GetComponent<Fuel>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    public virtual PlayerState HandleInput() {
        return this;
    }
}
