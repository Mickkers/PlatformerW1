using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI checkpointCount;
    [SerializeField] private TextMeshProUGUI respawns;
    [SerializeField] private RectTransform healthImage;

    private PlayerHealth pHealth;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = FindObjectOfType<PlayerHealth>();
        gameManager = FindAnyObjectByType<GameManager>();
        levelText.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        checkpointCount.text = "CPs: " + gameManager.GetCollectedCheckpoints() + " / " + gameManager.GetTotalCheckpoints();
        respawns.text = "Respawns: " + gameManager.GetRespawnsAvailable();
    }

    private void UpdateHealthUI()
    {
        healthImage.sizeDelta = new Vector2(196 * pHealth.GetHealthPercentage(), 64);
    }
}
