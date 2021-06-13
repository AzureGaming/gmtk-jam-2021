using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    public delegate void WinGame();
    public static WinGame OnWinGame;
    public delegate void LoseGame();
    public static LoseGame OnLoseGame;
    public delegate void StartGame();
    public static StartGame OnStartGame;
    public delegate void Clear();
    public static Clear OnClear;

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject taskList;
    public GameObject oxygenDisplay;

    private void OnEnable() {
        OnWinGame += DisplayWinScreen;
        OnLoseGame += DisplayLoseScreen;
        OnClear += DisplayNone;
        OnStartGame += DisplayGame;
    }

    private void OnDisable() {
        OnWinGame -= DisplayWinScreen;
        OnLoseGame -= DisplayLoseScreen;
        OnClear -= DisplayNone;
        OnStartGame -= DisplayGame;
    }

    void DisplayWinScreen() {
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
        taskList.SetActive(false);
        oxygenDisplay.SetActive(false);
    }

    void DisplayLoseScreen() {
        winScreen.SetActive(false);
        loseScreen.SetActive(true);
        taskList.SetActive(false);
        oxygenDisplay.SetActive(false);
    }

    void DisplayGame() {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        taskList.SetActive(true);
        oxygenDisplay.SetActive(true);
    }

    void DisplayNone() {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        taskList.SetActive(false);
        oxygenDisplay.SetActive(false);
    }
}
