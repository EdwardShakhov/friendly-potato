using System.Collections;
using System.Collections.Generic;
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
        while (_enemyCount < GameManager.EnemyCountMax)
        {
            Instantiate(_enemyPrefab, new Vector3(Random.Range(-1f, 1f) * GameManager.MapSize, 0, Random.Range(-1f, 1f) * GameManager.MapSize), Quaternion.identity);
            yield return new WaitForSeconds(_enemySpawnTime);
            _enemyCount ++;
        }

    }
}
