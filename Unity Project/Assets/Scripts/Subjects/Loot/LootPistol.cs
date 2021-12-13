using Player;
using UnityEngine;

public class LootPistol : MonoBehaviour
{
    protected void Update()
    {
        Destroy(gameObject, 20f);
        
        var player = GameManager.Instance.Player.GetComponent<PlayerController>();

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 1.5f)
        {
            Destroy(gameObject);
            GameManager.Instance.Player.GetComponent<PlayerSound>().Loot();

            var weapons = GameManager.Instance.Player.GetComponent<PlayerController>().Weapons;
            foreach (var weapon in weapons)
            {
                if (weapon.GetComponent<Weapon>().WeaponName == "Pistol")
                {
                    weapon.GetComponent<Weapon>().NumberOfBullets += 12;
                }
            }
        }
    }
}