using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour {
    public virtual PlayerState HandleInput() {
        return this;
    }
}
