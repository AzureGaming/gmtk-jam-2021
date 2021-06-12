using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour {
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
        CanvasManager.OnClear?.Invoke();
    }

    public void PlayAgain() {
        SceneManager.LoadScene("Game");
        CanvasManager.OnClear?.Invoke();
    }
}
