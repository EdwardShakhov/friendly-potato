﻿using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponHUD : MonoBehaviour
{
    [SerializeField] private GameObject _gunIcon;
    [SerializeField] private Dictionary<string, Sprite> _spriteDictionary;

    void Start()
    {
        _spriteDictionary ??= new Dictionary<string, Sprite>();
        _spriteDictionary.Add("Pistol", Resources.Load<Sprite>("Materials/UI/Pistol_Icon"));
        _spriteDictionary.Add("ShotGun", Resources.Load<Sprite>("Materials/UI/Shotgun_Icon"));
        ReloadIcon();
    }

    public void ReloadIcon()
    {
        _gunIcon.GetComponent<Image>().sprite = _spriteDictionary[GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon.WeaponName];
    }
}
