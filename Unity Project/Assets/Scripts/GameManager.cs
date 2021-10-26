using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public HealthBar HealthBar;
    public int MaxHealth = 100;
    public int CurrentHealth;
    
    [Header("Enemies")]
    public GameObject SpawnEnemies;
    public List<GameObject> EnemiesList;
    public int MapSize = 90;
    public int EnemyCountMax = 100;



    
    protected void Start()
    {
        Player = Instantiate(Player);
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
        
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

    public void AddEnemy(GameObject enemy)
    {
        if (EnemiesList == null)
        {
            EnemiesList = new List<GameObject>();
        }
        EnemiesList.Add(enemy);
    }
}