using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    public delegate void WinGame();
    public static WinGame OnWinGame;
    public delegate void LoseGame();
    public static LoseGame OnLoseGame;
    public delegate void Clear();
    public static Clear OnClear;

    public GameObject winScreen;
    public GameObject loseScreen;

    private void OnEnable() {
        OnWinGame += DisplayWinScreen;
        OnLoseGame += DisplayLoseScreen;
        OnClear += DisplayNone;
    }

    private void OnDisable() {
        OnWinGame -= DisplayWinScreen;
        OnLoseGame -= DisplayLoseScreen;
        OnClear -= DisplayNone;
    }

    void DisplayWinScreen() {
        winScreen.SetActive(true);
    }

    void DisplayLoseScreen() {
        loseScreen.SetActive(true);
    }

    void DisplayNone() {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
}
