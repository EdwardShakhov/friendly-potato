using UnityEngine;

public class WeaponSwitch : MonoBehaviour
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
        
        var previousSelectedWeapon = SelectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
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
            if (SelectedWeapon <= 0)
            {
                _selectedWeapon = transform.childCount - 1;
            }
            else
            {
                _selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon ()
    {
        var i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == SelectedWeapon);
            i++;
        }
    }
}
