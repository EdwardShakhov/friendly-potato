using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;

    protected void Start()
    {
        SetMaxHealth(PlayerController.PlayerMaxHealth);
    }

    protected void Update()
    {
        SetHealth(PlayerController.PlayerHealth);
    }
    
    private void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    private void SetHealth(int health)
    {
        if (Mathf.Abs(slider.value - health) < 0.001f)
        {
            return;
        }
        slider.value = health;
    }
}
