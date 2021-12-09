using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    [SerializeField] private float _multipleEnemyInstantiationDelay;
    [SerializeField] private float _oneEnemyInstantiationDelay;
    
    [SerializeField] private float _firstEnemyPositionX;
    [SerializeField] private float _firstEnemyPositionY;
    [SerializeField] private float _firstEnemyPositionZ;

    protected void Start()
    {
        StartCoroutine(EnemyInstantiation());
        Invoke(nameof(InstantiateOneEnemy), _oneEnemyInstantiationDelay);
    }
    
    private IEnumerator EnemyInstantiation()
    {
        while (GameManager.Instance.CurrentNumberOfEnemiesOnMap < GameManager.Instance.MaximumNumberOfEnemies)
        {
            Invoke(nameof(InstantiateManyEnemies), _multipleEnemyInstantiationDelay);
            GameManager.Instance.CurrentNumberOfEnemiesOnMap ++;
            yield return new WaitForSeconds(GameManager.Instance.EnemySpawnTime);
        }
    }
    
    private void InstantiateOneEnemy()
    {
        GameManager.Instance.EnemiesList(Instantiate(
            _enemyPrefab, new Vector3(_firstEnemyPositionX, _firstEnemyPositionY, _firstEnemyPositionZ), Quaternion.identity));
    }

    private void InstantiateManyEnemies()
    {
        GameManager.Instance.EnemiesList(Instantiate(_enemyPrefab, 
            new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 
                0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity));
    }
}
