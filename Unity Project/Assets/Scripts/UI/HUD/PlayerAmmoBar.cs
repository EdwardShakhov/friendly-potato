using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    public Slider Slider => slider;

    protected void Update()
    {
        SetReload(GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.CurrentBar);
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
}
