using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public static void DamagePlayer(short damage)
    {
        GameManager.Instance.PlayerHealth -= damage;
        GameManager.Instance.HealthBar.SetHealth(GameManager.Instance.PlayerHealth);
    }
    
    public static void DamageEnemy(short damage)
    {
        GameManager.Instance.EnemyHealth -= damage;
        GameManager.Instance.HealthBar.SetHealth(GameManager.Instance.EnemyHealth);
    }
}
