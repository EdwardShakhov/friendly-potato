using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    protected void Start()
    {
        SetMaxHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
    }

    protected void Update()
    {
        SetMaxHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
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

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
