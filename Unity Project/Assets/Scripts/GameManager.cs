using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int _lootInstantiationTime = 15;

    [Header("Instantiated Objects")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _spawnEnemies;
    private List<GameObject> Enemies;

    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _spawnEnemiesPrefab;
    
    [SerializeField] private GameObject _healthKit;
    [SerializeField] private GameObject _ammoPistolKit;
    [SerializeField] private GameObject _ammoShotgunKit;

    [Header("UI")]
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    [SerializeField] private PlayerAmmoBar _playerAmmoBar;
    [SerializeField] private PlayerExperienceBar _playerExperienceBar;
    [SerializeField] private PlayerWeaponHUD _playerWeaponHUD;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    private const int _showScreenTime = 5;
    
    //getters/setters
    public int LootInstantiationTime => _lootInstantiationTime;
    public GameObject HealthKit => _healthKit;
    public GameObject AmmoPistolKit => _ammoPistolKit;
    public GameObject AmmoShotgunKit => _ammoShotgunKit;
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
    public PlayerAmmoBar PlayerAmmoBar => _playerAmmoBar;
    public PlayerWeaponHUD PlayerWeaponHUD => _playerWeaponHUD;
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
        _playerHealthBar.Show();
        _playerAmmoBar.Show();
        _playerExperienceBar.Show();
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
        Invoke(nameof(GameOverScreenSetActive), _showScreenTime);
    }

    public void GameOverScreenSetActive()
    {
        _gameOverScreen.SetActive();
    }
}