using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PauseScreen;

public class GameManager : MonoBehaviour
{
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
    
    [Header("UI")]
    private PlayerHealthBar _playerHealthBar;
    private PlayerAmmoBar _playerAmmoBar;
    private GameOverScreen _gameOverScreen;
    private PauseScreen _pauseScreen;
    public bool IsGamePaused;

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
    
    protected void Start()
    {
        GameInit();
    }

    private void GameInit()
    {
        Player = Instantiate(Player);
        SpawnEnemies = Instantiate(SpawnEnemies);
        _playerHealthBar.Show(); //объединить - Hud.Show();
        _playerAmmoBar.Show();
        IsPlayerDead = false;
        IsGamePaused = false;
    }

    public void GamePause()
    {
        _pauseScreen.SetActive();
    }

    public void GameOver()
    {
        IsPlayerDead = true;
        Invoke(nameof(GameOverScreenSetActive), 5f);
    }

    public void GameOverScreenSetActive()
    {
        _gameOverScreen.SetActive();
    }
}