using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject pauseUI;

    private InputManager playerInput;
    private bool isPaused;

    private void Start()
    {
        playerInput = FindObjectOfType<InputManager>();
        isPaused = false;
    }

    public void EnableGameover()
    {
        gameoverUI.SetActive(true);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);
        gameplayUI.SetActive(!isPaused);
        if (isPaused)
        {
            playerInput.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            playerInput.enabled = true;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
