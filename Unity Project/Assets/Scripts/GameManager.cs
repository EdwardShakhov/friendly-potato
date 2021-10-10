using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int PlayerHealthPoint { get; set; }
    public const int MapSize = 90;
    public const int EnemyCountMax = 100;
    
    protected void Start()
    {
        PlayerHealthPoint = 100;
    }
    
    //Singleton
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