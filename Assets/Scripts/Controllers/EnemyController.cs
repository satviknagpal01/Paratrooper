using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public GameObject enemyPrefab;
    public int enemyPoolSize = 16;
    public GameObject[] enemies;
    private int currentEnemy = 0;

    public List<Enemy> enemyOnLeft, enemyOnRight;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        enemies = new GameObject[enemyPoolSize];
        for (int i = 0; i < enemyPoolSize; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, transform);
            enemies[i].SetActive(false);
        }
    }

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        currentEnemy++;
        enemies[currentEnemy].transform.position = spawnPosition;
        enemies[currentEnemy].SetActive(true);

        if(currentEnemy >= enemyPoolSize - 1)
        {
            currentEnemy = 0;
        }
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.InGame)
        {
            if (enemyOnLeft.Count > 3)
            {
                foreach (var enemy in enemyOnLeft)
                {
                    StartAttackFromLeft();
                }
            }
            if (enemyOnRight.Count > 3)
            {
                foreach (var enemy in enemyOnRight)
                {
                    StartAttackFromRight();
                }
            }
        }
    }

    void StartAttackFromLeft()
    {
        enemyOnLeft[0].SetState(EnemyStates.attack);
    }

    void StartAttackFromRight()
    {
        enemyOnRight[0].SetState(EnemyStates.attack);
    }
}