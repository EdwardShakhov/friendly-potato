using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    public Slider slider;

    protected void Start()
    {
        NoReload(PlayerShoot.ReloadBar = 6);
    }

    protected void Update()
    {
        SetReload(PlayerShoot.ReloadBar);
    }


    public void NoReload(float reload)
    {
        slider.maxValue = reload;
        slider.value = reload;
    }
    
    public void SetReload(float reload)
    {
        slider.value = reload;
    }
}
