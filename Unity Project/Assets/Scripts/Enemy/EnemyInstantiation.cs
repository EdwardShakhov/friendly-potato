using System.Collections;
using UnityEngine;

public class EnemyInstantiation : MonoBehaviour
{
    [SerializeField] private int _enemyCount;

    protected void Start()
    {
        GameManager.Instance.EnemyHealth = GameManager.Instance.EnemyMaxHealth;
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (_enemyCount < GameManager.Instance.MaximumNumberOfEnemies)
        {
            Instantiate(GameManager.Instance.Enemy, new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 
                0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity);
            yield return new WaitForSeconds(GameManager.Instance.EnemySpawnTime);
            _enemyCount ++;
        }

    }
}
