using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float speed = -3f;
    private int remainingEnemySpawns;
    private bool hasSpawnedEnemies = false;

    private void OnEnable()
    {
        remainingEnemySpawns = Random.Range(1, 3);
    }

    void Update()
    {
        MoveHelicopter();

        if (IsOutOfRange())
        {
            gameObject.SetActive(false);
        }

        TrySpawnEnemies();
    }

    void MoveHelicopter()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    bool IsOutOfRange()
    {
        return transform.position.x > 11f || transform.position.x < -11f;
    }

    void TrySpawnEnemies()
    {
        if (remainingEnemySpawns > 0 && !hasSpawnedEnemies)
        {
            float spawnDelay = Random.Range(1f, 3f);
            Invoke(nameof(SpawnEnemy), spawnDelay);
            hasSpawnedEnemies = true;
            remainingEnemySpawns--;
        }
    }

    void SpawnEnemy()
    {
        if (IsSuitableForEnemySpawn())
        {
            Vector3 enemySpawnPosition = transform.position - new Vector3(0, 0.3f, 0);
            EnemyController.instance.SpawnEnemy(enemySpawnPosition);
            hasSpawnedEnemies = false;
        }
    }

    bool IsSuitableForEnemySpawn()
    {
        return ((transform.position.x > -8f && transform.position.x < -1.25f ) ||
               (transform.position.x > 1.25f && transform.position.x < 8f));
    }
}
