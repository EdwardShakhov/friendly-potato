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
    [SerializeField] private bool _isGamePaused;
    [SerializeField] private bool _isPlayerDead;

    [Header("Level Essentials")]
    [SerializeField] private int _currentNumberOfEnemiesOnMap;
    [SerializeField] private int _maximumNumberOfEnemies = 50;
    [SerializeField] private int _enemySpawnTime = 1;
    [SerializeField] private int _mapSize = 90;

    [Header("Instantiated Objects")]
    [SerializeField] private GameObject _player;
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
    
    //getters/setters
    public bool IsGamePaused
    {
        get => _isGamePaused;
        set => _isGamePaused = value;
    }
    public bool IsPlayerDead
    {
        get => _isPlayerDead;
        set => _isPlayerDead = value;
    }
    public int CurrentNumberOfEnemiesOnMap
    {
        get => _currentNumberOfEnemiesOnMap;
        set => _currentNumberOfEnemiesOnMap = value;
    }
    public int MaximumNumberOfEnemies => _maximumNumberOfEnemies;
    public int EnemySpawnTime => _enemySpawnTime;
    public int MapSize => _mapSize;
    public GameObject Player => _player;
    //getters/setters end
    
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
        _player = Instantiate(_playerPrefab);
        _spawnEnemies = Instantiate(_spawnEnemiesPrefab);
        _playerHealthBar.Show(); //объединить - Hud.Show();
        _playerAmmoBar.Show();
        _isPlayerDead = false;
        _isGamePaused = false;
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
        _isPlayerDead = true;
        Invoke(nameof(GameOverScreenSetActive), 5f);
    }

    public void GameOverScreenSetActive()
    {
        _gameOverScreen.SetActive();
    }
}