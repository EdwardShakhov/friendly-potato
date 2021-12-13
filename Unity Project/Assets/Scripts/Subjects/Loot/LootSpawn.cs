using System.Collections;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    private GameObject _healthKit;
    private GameObject _ammoPistolKit;
    private GameObject _ammoShotgunKit;
    
    private Vector3 _positionHealthkit = new Vector3 (-11f, 1f, 4.5f);
    private Vector3 _positionAmmoPistolKit = new Vector3 (-10.75f, 1f, 5.5f);
    private Vector3 _positionAmmoShotgunKit = new Vector3 (-10.5f, 1f, 6.5f);

    protected void Awake()
    {
        _healthKit = _healthKit ? _healthKit : GameManager.Instance.HealthKit;
        _ammoPistolKit = _ammoPistolKit ? _ammoPistolKit : GameManager.Instance.AmmoPistolKit;
        _ammoShotgunKit = _ammoShotgunKit ? _ammoShotgunKit : GameManager.Instance.AmmoShotgunKit;
    }

    protected void Start()
    {
        StartCoroutine(InstantiateLootOnMap());
    }

    private IEnumerator InstantiateLootOnMap()
    {
        var time = GameManager.Instance.LootInstantiationTime;
        while (!GameManager.Instance.IsGamePaused)
        {
            Destroy(Instantiate(_healthKit, _positionHealthkit, Quaternion.identity), time);
            Destroy(Instantiate(_ammoPistolKit, _positionAmmoPistolKit, Quaternion.identity), time);
            Destroy(Instantiate(_ammoShotgunKit, _positionAmmoShotgunKit, Quaternion.identity), time);
                    
            yield return new WaitForSeconds(time);
        }
    }
}
