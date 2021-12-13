using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spiderPrefab;
    
    private Vector3 _firstZombiePosition = new Vector3(-6f,0f,4.3f);
    private Vector3 _firstSpiderPosition = new Vector3(20f,0f,5f);

    protected void Start()
    {
        StartCoroutine(ZombieInstantiation());
        StartCoroutine(SpiderInstantiation());
        Invoke(nameof(InstantiateOneZombie), 2.5f);
        Invoke(nameof(InstantiateOneSpider), 5f);
    }
    
    private IEnumerator ZombieInstantiation()
    {
        while (GameManager.Instance.CurrentNumberOfZombiesOnMap < GameManager.Instance.MaximumNumberOfZombies)
        {
            InstantiateManyZombies();
            GameManager.Instance.CurrentNumberOfZombiesOnMap++;
            yield return new WaitForSeconds(GameManager.Instance.ZombieSpawnTime);
        }
    }

    private IEnumerator SpiderInstantiation()
    {
        while (GameManager.Instance.CurrentNumberOfSpidersOnMap < GameManager.Instance.MaximumNumberOfSpiders)
        {
            if (GameManager.Instance.Key.GetComponent<LevelKey>().IsTaken)
            {
                InstantiateManySpiders();
                GameManager.Instance.CurrentNumberOfSpidersOnMap++;
            }
            yield return new WaitForSeconds(GameManager.Instance.SpiderSpawnTime);
        }
    }

    private void InstantiateOneZombie()
    {
        GameManager.Instance.EnemiesList(Instantiate(_enemyPrefab, _firstZombiePosition, Quaternion.identity));
        GameManager.Instance.CurrentNumberOfZombiesOnMap++;
    }

    private void InstantiateManyZombies()
    {
        GameManager.Instance.EnemiesList(Instantiate(_enemyPrefab, 
            new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 
                0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity));
    }

    private void InstantiateOneSpider()
    {
        GameManager.Instance.EnemiesList(Instantiate(_spiderPrefab, _firstSpiderPosition, Quaternion.identity));
        GameManager.Instance.CurrentNumberOfSpidersOnMap++;
    }
    
    private void InstantiateManySpiders()
    {
        GameManager.Instance.EnemiesList(Instantiate(_spiderPrefab, 
            new Vector3(Random.Range(-1f, 1f) * GameManager.Instance.MapSize, 
                0, Random.Range(-1f, 1f) * GameManager.Instance.MapSize), Quaternion.identity));
    }
}
