using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsUI;

    private bool isSettingsOpen = false;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsUI.SetActive(isSettingsOpen);
    }
}
