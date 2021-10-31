using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public  Slider slider;

    protected void Start()
    {
        SetMaxHealth(GameManager.Instance.PlayerMaxHealth);
    }

    protected void Update()
    {
        SetHealth(GameManager.Instance.PlayerHealth);
    }


    public  void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public  void SetHealth(int health)
    {
        slider.value = health;
    }
}
