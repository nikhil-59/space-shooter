using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawn = false;
    [SerializeField]
    private GameObject[] _powerups;
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawn)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-10f, 10f), 8f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawn) {
            Instantiate(_powerups[Random.Range(0,_powerups.Length)], new Vector3(Random.Range(-10f, 10f), 8f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4.0f, 7.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        Destroy(_enemyContainer.gameObject);
    }
}
