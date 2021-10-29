using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    protected void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
