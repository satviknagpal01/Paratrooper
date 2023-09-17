using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public static HelicopterController instance;
    public GameObject helicopterPrefab;
    public int helicopterPoolSize = 5;
    public float spawnRate = 5f;
    public float spawnHeight = 5f;
    public float spawnXOffset = 5f;
    public float spawnXRange = 5f;

    private ObjectPool<Helicopter> helicopterPool;
    private int currentHelicopter = 0;
    private float spawnTimer = 0f;
    private int spawnXPosition = 0;
    private Vector3 spawnPosition;
    private bool isSpawning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        helicopterPool = new ObjectPool<Helicopter>(helicopterPrefab.GetComponent<Helicopter>(), transform, helicopterPoolSize);
    }

    private void OnEnable()
    {
        StartSpawning();
    }

    void Update()
    {
        if (GameManager.instance.state == GameState.InGame)
        {
            if (isSpawning)
            {
                if (spawnTimer > spawnRate)
                {
                    SpawnHelicopter();
                    spawnTimer = 0f;
                    spawnRate = Random.Range(2f, 6f);
                }
                else
                {
                    spawnTimer += Time.deltaTime;
                }
            }
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void SpawnHelicopter()
    {
        spawnXPosition = Random.Range(0, 2) == 0 ? -10 : 10;
        spawnPosition = new Vector3(spawnXPosition, 4f, 0);
        var helicopter = helicopterPool.GetObject();
        helicopter.transform.position = spawnPosition;

        if (spawnXPosition == -10)
            helicopter.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            helicopter.transform.rotation = Quaternion.Euler(0, 0, 0);

        currentHelicopter++;
        if (currentHelicopter >= helicopterPoolSize)
        {
            currentHelicopter = 0;
        }
    }

    public void Reset()
    {
        StopSpawning();
        currentHelicopter = 0;
        helicopterPool.ReturnAllObjects();
        spawnTimer = 0f;
    }
}
