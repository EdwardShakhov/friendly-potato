using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private float _reloadBarPistol;
    private float _reloadBarShotgun;

    protected void Update()
    {
        if (GameManager.Instance.Player.GetComponent<PlayerController>().Weapon.GetComponent<WeaponSwitch>().SelectedWeapon == 0)
        {
            _reloadBarPistol = GameObject.Find("Player(Clone)/DummyRig/root/B-hips/B-spine/B-chest/B-upperChest/B-shoulder_R/B-upper_arm_R/B-forearm_R/B-hand_R/AllWeapons(Clone)/Pistol/SpawnPoint")
                .GetComponent<PlayerShoot>().ReloadBar;
            SetReload(_reloadBarPistol);
        }

        if (GameManager.Instance.Player.GetComponent<PlayerController>().Weapon.GetComponent<WeaponSwitch>().SelectedWeapon == 1)
        {
            _reloadBarShotgun = GameObject.Find("Player(Clone)/DummyRig/root/B-hips/B-spine/B-chest/B-upperChest/B-shoulder_R/B-upper_arm_R/B-forearm_R/B-hand_R/AllWeapons(Clone)/Shotgun/SpawnPoint")
                .GetComponent<PlayerShoot>().ReloadBar;
            SetReload(_reloadBarShotgun);
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
    
    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
