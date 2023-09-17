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

    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        bulletPool = new ObjectPool<Bullet>(bulletPrefab.GetComponent<Bullet>(), transform, bulletPoolSize);
    }


    public void SpawnBullet(Vector2 position, Vector2 direction)
    {
        // Use the object pool to get a bullet.
        Bullet bullet = bulletPool.GetObject();
        bullet.transform.position = position;
        bullet.gameObject.SetActive(true);

        bullet.Shoot(direction);
    }

    public void Reset()
    {
        bulletPool.ReturnAllObjects();
    }
}
