using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    public TextMeshProUGUI Bullets;
    
    public Slider Slider => slider;

    protected void Update()
    {
        SetReload(GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.CurrentBar);
        ShowNumberOfBullets(GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.NumberOfBullets,
            GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.MaxNumberOfBullets);
        if (GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.NumberOfBullets == 0)
        {
            slider.value = 0;
        }
    }

    private void SetReload(float reload)
    {
        if (Mathf.Abs(slider.value - reload) < 0.001f)
        {
            return;
        }
        slider.value = reload;
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void ShowNumberOfBullets(int bullet, int maxBullets)
    {
        Bullets.text = bullet + " / " + maxBullets;
    }
}
