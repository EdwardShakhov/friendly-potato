using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public TextMeshProUGUI Health;
    
    protected void Start()
    {
        SetMaxHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
    }

    protected void Update()
    {
        SetMaxHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
        SetHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerHealth);
        ShowNumberOfHealth(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerHealth,
                GameManager.Instance.Player.GetComponent<PlayerController>().PlayerMaxHealth);
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
    
    private void ShowNumberOfHealth(int health, int maxHealth)
    {
        Health.text = health + " / " + maxHealth;
    }
}
