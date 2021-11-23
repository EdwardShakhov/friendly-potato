using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponHUD : MonoBehaviour
{
    [SerializeField] private GameObject _pistolIcon;
    [SerializeField] private GameObject _shotgunIcon;

    public GameObject PistolIcon => _pistolIcon;
    public GameObject ShotgunIcon => _shotgunIcon;

}
