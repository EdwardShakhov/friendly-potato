using System.Collections;
using UnityEngine;

public class EnemyInstantiation : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _enemySpawnTime = 1.0f;
    [SerializeField] private int _enemyCount;

    protected void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (_enemyCount < GameManager.Instance.EnemyCountMax)
        {
            GameManager.Instance.AddEnemy(Instantiate(_enemyPrefab, new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity));
            yield return new WaitForSeconds(_enemySpawnTime);
            _enemyCount ++;
        }

    }
}
