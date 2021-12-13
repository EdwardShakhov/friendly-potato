using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponHUD : MonoBehaviour
{
    [SerializeField] private GameObject _gunIcon;
    private Dictionary<string, Sprite> _spriteDictionary;

    public GameObject GunIcon => _gunIcon;

    void Start()
    {
        _spriteDictionary ??= new Dictionary<string, Sprite>();
        _spriteDictionary.Add("Pistol", Resources.Load<Sprite>("Materials/UI/Pistol_Icon"));
        _spriteDictionary.Add("Shotgun", Resources.Load<Sprite>("Materials/UI/Shotgun_Icon"));
        ReloadIcon();
    }

    public void ReloadIcon()
    {
        _gunIcon.GetComponent<Image>().sprite = 
            _spriteDictionary[GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.WeaponName];
    }
}
