using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    public GameObject enemy;
    public int x;
    public int z;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCount < 100)
        {
            x = Random.Range(-90, 90);
            z = Random.Range(-90, 90);
            Instantiate(enemy, new Vector3(x, 0, z), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            enemyCount += 1;
        }

    }
}
