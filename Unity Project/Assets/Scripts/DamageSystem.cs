using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public static void DamagePlayer(short damage)
    {
        GameManager.Instance.CurrentHealth -= damage;
        GameManager.Instance.HealthBar.SetHealth(GameManager.Instance.CurrentHealth);
    }
}
