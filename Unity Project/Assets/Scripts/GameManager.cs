using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Enemies;
    public int MapSize = 90;
    public int EnemyCountMax = 100;
    
    
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
        if (Enemies == null)
        {
            Enemies = new List<GameObject>();
        }
        Enemies.Add(enemy);
    }
}