using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public HealthBar HealthBar;
    public short PlayerMaxHealth = 1000;
    public short PlayerHealth;
    
    [Header("Enemies")]
    public GameObject Enemy;
    public GameObject SpawnEnemies;
    public short EnemyMaxHealth = 1000;
    public short EnemyHealth;
    //public List<GameObject> EnemiesList;
    public int MaximumNumberOfEnemies = 50;
    public int EnemySpawnTime;
    public const float EnemyAttackDistance = 1.3f;

    [Header("Map")]
    public int MapSize = 90;
    
    protected void Start()
    {
        Player = Instantiate(Player);
        PlayerHealth = PlayerMaxHealth;
        HealthBar.SetMaxHealth(PlayerMaxHealth);
        
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

    /*public void AddEnemy(GameObject enemy)
    {
        if (EnemiesList == null)
        {
            EnemiesList = new List<GameObject>();
        }
        EnemiesList.Add(enemy);
    }*/
}