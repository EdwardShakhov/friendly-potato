using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    protected void Start()
    {
        StartCoroutine(EnemyInstantiation());
    }

    private IEnumerator EnemyInstantiation()
    {
        while (GameManager.Instance.NumberOfEnemies < GameManager.Instance.MaximumNumberOfEnemies)
        {
            GameManager.Instance.EnemiesList(Instantiate(_enemyPrefab, 
                new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 
                    0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity));
            GameManager.Instance.NumberOfEnemies ++;
            yield return new WaitForSeconds(GameManager.Instance.EnemySpawnTime);
        }

    }
}
