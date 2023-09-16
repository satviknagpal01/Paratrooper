using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    //pool of helicopters
    public static HelicopterController instance;
    public GameObject helicopterPrefab;
    public int helicopterPoolSize = 5;
    public float spawnRate = 5f;
    public float spawnHeight = 5f;
    public float spawnXOffset = 5f;
    public float spawnXRange = 5f;
    

    private GameObject[] helicopters;
    private int currentHelicopter = 0;
    private float spawnTimer = 0f;
    private int spawnXPosition = 0;
    private Vector3 spawnPosition;
    private bool isSpawning = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //create pool of helicopters
        helicopters = new GameObject[helicopterPoolSize];
        for (int i = 0; i < helicopterPoolSize; i++)
        {
            helicopters[i] = Instantiate(helicopterPrefab, transform);
            helicopters[i].SetActive(false);
        }
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
        Debug.Log($"Now Spawning Helicopter : {currentHelicopter}");
        spawnXPosition = (int)Random.Range(0, 2);
        if (spawnXPosition == 0)
        {
            spawnXPosition = 10;
        }
        else
        {
            spawnXPosition = -10;
            helicopters[currentHelicopter].transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        spawnPosition = new Vector3(spawnXPosition, 4, 0);
        helicopters[currentHelicopter].transform.position = spawnPosition;
        helicopters[currentHelicopter].SetActive(true);
        currentHelicopter++;
        if (currentHelicopter >= helicopterPoolSize)
        {
            currentHelicopter = 0;
        }
    }

}
