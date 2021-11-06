using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public bool IsPlayerDead;
    
    [Header("Enemies")]
    public GameObject Enemy;
    public GameObject SpawnEnemies;
    public int MaximumNumberOfEnemies = 50;
    public int EnemySpawnTime;
    public const float EnemyAttackDistance = 1.3f;

    [Header("Map")]
    public int MapSize = 90;
    
    protected void Start()
    {
        GameInit();
    }

    private void GameInit()
    {
        Player = Instantiate(Player);
        SpawnEnemies = Instantiate(SpawnEnemies);
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager instance not specified");
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        _instance = this;
    }

    protected void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}