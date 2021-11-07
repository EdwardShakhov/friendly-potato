using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameOverScreen GameOverScreen;

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

    public void GameOver()
    {
        Invoke(nameof(GameOverScreenSetActive), 5f);
    }

    public void GameOverScreenSetActive()
    {
        GameOverScreen.SetActive();
    }

    /*public void GameOver()
    {
        if (IsPlayerDead)
        {
            Debug.Log("Game Over");
            Invoke(nameof(RestartLevel), 5f);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}