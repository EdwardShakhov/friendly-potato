using Player;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private int _selectedWeapon;

    public int SelectedWeapon => _selectedWeapon;

    protected void Start()
    {
        SelectWeapon();
    }

    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) return;
        
        var previousSelectedWeapon = _selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_selectedWeapon >= transform.childCount - 1)
            {
                _selectedWeapon = 0;
            }
            else
            {
                _selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (_selectedWeapon <= 0)
            {
                _selectedWeapon = transform.childCount - 1;
            }
            else
            {
                _selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon ()
    {
        if(!GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.BarIsNotEmpty) return;
        
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        
        var i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == _selectedWeapon);
            i++;
        }
        
        playerController.ActiveWeapon = playerController.Weapons[_selectedWeapon].GetComponent<Weapon>();
        GameManager.Instance.PlayerAmmoBar.Slider.maxValue = playerController.ActiveWeapon.BarCapacity;

        switch (playerController.AllWeapons.GetComponent<WeaponController>().SelectedWeapon)
        {
            case 0:
                GameManager.Instance.PlayerWeaponHUD.PistolIcon.SetActive(true);
                GameManager.Instance.PlayerWeaponHUD.ShotgunIcon.SetActive(false);
                break;
            case 1:
                GameManager.Instance.PlayerWeaponHUD.PistolIcon.SetActive(false);
                GameManager.Instance.PlayerWeaponHUD.ShotgunIcon.SetActive(true);
                break;
        }
    }
}
