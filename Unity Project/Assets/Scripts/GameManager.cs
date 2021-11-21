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
    
    [Header("Game State")]
    [SerializeField] protected internal bool IsGamePaused;
    [SerializeField] protected internal bool IsPlayerDead;

    [Header("Level Essentials")]
    [SerializeField] protected internal int NumberOfEnemies;
    [SerializeField] protected internal int MapSize = 90;
    [SerializeField] protected internal int MaximumNumberOfEnemies = 50;
    [SerializeField] protected internal int EnemySpawnTime = 1;

    [Header("Instantiated Objects")]
    [SerializeField] protected internal GameObject Player;
    [SerializeField] private GameObject _spawnEnemies;
    private List<GameObject> Enemies; //[SerializeField]

    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _spawnEnemiesPrefab;

    [Header("UI")]
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    [SerializeField] private PlayerAmmoBar _playerAmmoBar;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private PauseScreen _pauseScreen;
    
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
        Player = Instantiate(_playerPrefab);
        _spawnEnemies = Instantiate(_spawnEnemiesPrefab);
        _playerHealthBar.Show(); //объединить - Hud.Show();
        _playerAmmoBar.Show();
        IsPlayerDead = false;
        IsGamePaused = false;
    }
    
    public void EnemiesList(GameObject enemy)
    {
        Enemies ??= new List<GameObject>();
        Enemies.Add(enemy);
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