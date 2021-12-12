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

            var i = GameManager.Instance.Player.GetComponent<PlayerController>().Weapons;
            foreach (var weapon in i)
            {
                if (weapon.GetComponent<Weapon>().WeaponName == "Pistol")
                {
                    weapon.GetComponent<Weapon>().NumberOfBullets += 12;
                }
            }
        }
    }
}