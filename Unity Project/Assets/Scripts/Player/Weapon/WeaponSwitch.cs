using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public static int SelectedWeapon;

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
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount - 1;
            }
            else
            {
                SelectedWeapon--;
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
