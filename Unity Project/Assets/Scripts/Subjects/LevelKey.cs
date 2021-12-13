using Player;
using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private bool _isTaken;

    public bool IsTaken => _isTaken;

    protected void Awake()
    {
        _isTaken = false;
    }

    protected void Update()
    {
        var player = GameManager.Instance.Player.GetComponent<PlayerController>();

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 1.5f)
        {
            _isTaken = true;
            GameManager.Instance.Player.GetComponent<PlayerSound>().Loot();
            gameObject.SetActive(false);
        }
    }
}