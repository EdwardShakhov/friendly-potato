using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public void SetActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.IsGamePaused)
            {
                Pause();
            }
            else
            {
                ResumeButton();
            }
        }
    }

    private void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.IsGamePaused = true;
    }

    public void ResumeButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.IsGamePaused = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameManager.Instance.IsGamePaused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}