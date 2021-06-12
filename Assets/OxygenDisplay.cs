using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenDisplay : MonoBehaviour {
    public Oxygen oxygen;

    Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        slider.value = oxygen.value;
    }
}
