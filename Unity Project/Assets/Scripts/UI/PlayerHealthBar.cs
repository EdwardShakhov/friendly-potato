using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    
    protected void Start()
    {
        SetMaxHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
    }

    protected void Update()
    {
        SetHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerHealth);
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
