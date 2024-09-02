using UnityEngine;

public class EnemyEnergy : MonoBehaviour {

    public GameObject[] enemyBases;
    public float spawnTime = 2f;
    public float speed = 2f;
    public float rotationSpeed = 100f;

    private float screenMaxWidth;
    private float screenMaxHeight;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnTime);
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenMaxWidth = screenBounds.x * 2;
        screenMaxHeight = screenBounds.y * 2;
        foreach (var enemy in enemyBases)
        {
            enemy.SetActive(false);
        }
    }

    void SpawnEnemy()
    {
        int enemyTypeIndex = Random.Range(0, enemyBases.Length);
        GameObject enemyPrefab = enemyBases[enemyTypeIndex];

        GameObject enemy = Instantiate(enemyPrefab);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();

        enemyMovement.speed = speed;
        enemyMovement.rotationSpeed = rotationSpeed;
        
        Vector3 spawnPosition = GetRandomSpawnPosition(enemy.GetComponent<SpriteRenderer>().bounds.size);
        enemy.transform.position = spawnPosition;

        Color randomColor = new Color(Random.value, Random.value, Random.value);
        enemy.GetComponent<SpriteRenderer>().color = randomColor;


        enemy.SetActive(true);
    }

    Vector3 GetRandomSpawnPosition(Vector3 enemySize)
    {
        float xPos = Random.Range(-screenMaxWidth / 2 + enemySize.x / 2, screenMaxWidth / 2 - enemySize.x / 2);
        float yPos = Random.Range(-screenMaxHeight / 2 + enemySize.y / 2, screenMaxHeight / 2 - enemySize.y / 2);
        return new Vector3(xPos, yPos, 0);
    }

}