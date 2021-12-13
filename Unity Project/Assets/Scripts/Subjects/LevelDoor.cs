using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    private bool _canEnter;

    protected void Awake()
    {
        _canEnter = false;
    }

    protected void Update()
    {
        if (GameManager.Instance.Key.GetComponent<LevelKey>().IsTaken)
        {
            _canEnter = true;
        }
        
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) < 1.5f
            && _canEnter)
        {
            SceneManager.LoadScene("Scene_2");
        }
    }
}
