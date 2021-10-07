using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiation : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] public float waitDuration = 1.0f;
    [SerializeField] private int enemyCount;
    private const int EnemyCountMax = 100;

    protected void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (enemyCount < EnemyCountMax)
        {
            Instantiate(enemy, new Vector3(Random.Range(-90, 90), 0, Random.Range(-90, 90)), Quaternion.identity);
            yield return new WaitForSeconds(waitDuration);
            enemyCount ++;
        }

    }
}
