using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int _selectedWeapon;

    protected void Start()
    {
        SelectWeapon();
    }

    protected void Update()
    {
        var previousSelectedWeapon = _selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !GameManager.Instance.IsPlayerDead)
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
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !GameManager.Instance.IsPlayerDead)
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
        var i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == _selectedWeapon);
            i++;
        }
    }
}
