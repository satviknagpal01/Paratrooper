using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolController : MonoBehaviour
{
    public static BulletPoolController instance;
    public GameObject bulletPrefab;
    public int bulletPoolSize = 20;
    public float spawnRate = 0.5f;
    public float bulletMin = -2f;
    public float bulletMax = 2f;

    private GameObject[] bullets;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
    private int currentBullet = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        bullets = new GameObject[bulletPoolSize];
        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = (GameObject)Instantiate(bulletPrefab, objectPoolPosition, Quaternion.identity,transform);
            bullets[i].SetActive(false);
        }
    }

    public void SpawnBullet(Vector2 position, Vector2 direction)
    {
        bullets[currentBullet].transform.position = position;
        bullets[currentBullet].SetActive(true);

        bullets[currentBullet].GetComponent<Bullet>().Shoot(direction);
        currentBullet++;
        if (currentBullet >= bulletPoolSize)
        {
            currentBullet = 0;
        }
    }

}

