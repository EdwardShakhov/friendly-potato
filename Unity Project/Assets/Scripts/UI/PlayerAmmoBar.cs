using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    
    protected void Update()
    {
        SetReload(PlayerShoot.ReloadBar);
    }

    private void SetReload(float reload)
    {
        if (Mathf.Abs(slider.value - reload) < 0.001f)
        {
            return;
        }
        slider.value = reload;
    }
    
    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
