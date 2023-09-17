using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public GameObject enemyPrefab;
    public int enemyPoolSize = 16;

    private ObjectPool<Enemy> enemyPool;
    public List<Enemy> enemyOnLeft, enemyOnRight;
    private int leftEnemyAttacking = 0, rightEnemyAttacking = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        enemyPool = new ObjectPool<Enemy>(enemyPrefab.GetComponent<Enemy>(), transform, enemyPoolSize);
    }

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        Enemy enemy = enemyPool.GetObject();
        enemy.transform.position = spawnPosition;
        enemy.SetState(EnemyStates.Parachuting);
        enemy.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.InGame)
        {
            if (enemyOnLeft.Count > 3)
            {
                StartAttackFromLeft();
            }
            if (enemyOnRight.Count > 3)
            {
                StartAttackFromRight();
            }
            if(leftEnemyAttacking > 3 || rightEnemyAttacking >3)
            {
                GameManager.instance.UpdateGameState(GameState.GameOver);
                Reset();
            }
        }
    }

    void StartAttackFromLeft()
    {
        if (enemyOnLeft[leftEnemyAttacking].state == EnemyStates.TouchingBase)
            leftEnemyAttacking++;
        enemyOnLeft[leftEnemyAttacking].SetState(EnemyStates.Attacking);
    }

    void StartAttackFromRight()
    {
        if (enemyOnRight[rightEnemyAttacking].state == EnemyStates.TouchingBase)
            rightEnemyAttacking++;
        enemyOnRight[rightEnemyAttacking].SetState(EnemyStates.Attacking);
    }

    public void AddEnemyOnLeft(Enemy enemy)
    {
        enemyOnLeft.Add(enemy);
        enemyOnLeft.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
        enemyOnLeft.Reverse();
    }

    public void AddEnemyOnRight(Enemy enemy)
    {
        enemyOnRight.Add(enemy);
        enemyOnRight.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
    }
    
    public void Reset()
    {
        leftEnemyAttacking = 0;
        rightEnemyAttacking = 0;
        foreach (var enemy in enemyOnLeft)
        {
            enemy.SetState(EnemyStates.Parachuting);
        }
        enemyPool.ReturnAllObjects();
        enemyOnLeft.Clear();
        enemyOnRight.Clear();
    }

}
