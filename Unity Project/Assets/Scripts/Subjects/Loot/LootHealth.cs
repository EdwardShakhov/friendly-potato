using Player;
using UnityEngine;

public class LootHealth : MonoBehaviour
{
    private int _addHealth;
    
    protected void Update()
    {
        Destroy(gameObject, 20f);
        
        var player = GameManager.Instance.Player.GetComponent<PlayerController>();
        _addHealth = (int)(player.PlayerMaxHealth / 6f);
        
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 1.5f)
        {
            Destroy(gameObject);
            GameManager.Instance.Player.GetComponent<PlayerSound>().Loot();
            player.PlayerHealth += _addHealth;
        }
    }
}