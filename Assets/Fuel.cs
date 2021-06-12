using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {
    public bool hasFuel = false;
    public void Pickup() {
        hasFuel = true;
    }

    public void Drop() {
        hasFuel = false;
    }
}
