using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    public Slider slider;

    protected void Update()
    {
        SetReload(PlayerShoot.ReloadBar);
    }

    private void SetReload(float reload)
    {
        slider.value = reload;
    }
}
