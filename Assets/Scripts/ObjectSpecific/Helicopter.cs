using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float speed = -3f;
    private int enemySpawns;
    private bool hasSpawned = false;

    private void OnEnable()
    {
        enemySpawns = Random.Range(1, 3);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > 11f || transform.position.x < -11f)
        {
            gameObject.SetActive(false);
        }
        if (enemySpawns > 0)
        {
            if(!hasSpawned)
            {
                hasSpawned = true;
                Invoke(nameof(SpawnEnemy), Random.Range(1f, 3f));
                enemySpawns--;
            }
        }
    }

    void SpawnEnemy()
    {
        var pos = transform.position;
        pos.y -= 0.3f;
        if(transform.position.x < 8f && transform.position.x > -8f)
        {
            if(transform.position.x > 1.25f || transform.position.x < -1.25f)
            {
                EnemyController.instance.SpawnEnemy(pos);
                hasSpawned = false;
            }
        }
    }
}
