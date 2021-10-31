using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static Vector3 GetDirection()
    {
        if(!GameManager.Instance.IsPlayerDead)
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        }
        return default;
    }
}