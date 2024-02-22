using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int respawnsAvailable;
    [SerializeField] private Transform currRespawn;

    private UIManager ui;

    private Checkpoint[] checkpoints;
    private int checkpointCount;
    private int collectedCheckpoints = 0;
    private PlayerRespawn player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerRespawn>();
        ui = FindObjectOfType<UIManager>();
        CountTotalCheckpoints();
    }

    private void CountTotalCheckpoints()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        checkpointCount = checkpoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        ui.EnableGameover();
    }

    public void RespawnPlayer()
    {
        if(respawnsAvailable > 0)
        {
            StartCoroutine(player.Respawn());
            respawnsAvailable--;
        }
        else
        {
            GameOver();
        }
        
    }

    public void NewCheckpoint(Transform newCheckpoint)
    {
        currRespawn = newCheckpoint;
        collectedCheckpoints++;
    }

    public Transform GetCheckpoint()
    {
        return currRespawn;
    }

    public int GetCollectedCheckpoints()
    {
        return collectedCheckpoints;
    }

    public int GetTotalCheckpoints()
    {
        return checkpointCount;
    }

    public bool CanFinishLevel()
    {
        if(collectedCheckpoints == checkpointCount)
        {
            return true;
        }
        return false;
    }

    public int GetRespawnsAvailable()
    {
        return respawnsAvailable;
    }
}
