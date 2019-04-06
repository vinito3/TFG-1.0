using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start() {
        Resume();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
                Resume();
            else
                Pause();
        }

    }

    public void Pause() {
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Resume() {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void Disconnect() {
        SceneManager.LoadScene("Main Menu");
    }

}
