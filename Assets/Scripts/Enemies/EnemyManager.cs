using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyBases;
    public float spawnTime = 2f;
    public float speed = 2f;
    public float rotationSpeed = 100f;

    private float screenMaxWidth;
    private float screenMaxHeight;
    private bool isSpawning = true;
    private Coroutine spawnCoroutine;

    void Start()
    {
        SetScreenBounds();

        foreach (var enemy in enemyBases)
        {
            enemy.SetActive(false);
        }

        spawnTime = GameManager.Instance.spawnTime;
        spawnCoroutine = StartCoroutine(SpawnEnemyCoroutine());
    }

    void Update()
    {
        float newSpawnTime = GameManager.Instance.spawnTime;

        if (newSpawnTime != spawnTime)
        {
            spawnTime = newSpawnTime;
            RestartSpawning();
        }
    }

    private void SetScreenBounds()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenMaxWidth = screenBounds.x * 2;
        screenMaxHeight = screenBounds.y * 2;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnEnemy()
    {
        int enemyTypeIndex = Random.Range(0, enemyBases.Length);
        GameObject enemyPrefab = enemyBases[enemyTypeIndex];

        GameObject enemy = Instantiate(enemyPrefab);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.speed = speed;

        enemy.transform.position = GetRandomSpawnPosition(enemy.GetComponent<SpriteRenderer>().bounds.size);
        /*Color randomColor = new Color(Random.value, Random.value, Random.value);
        enemy.GetComponent<SpriteRenderer>().color = randomColor;*/
        enemy.GetComponent<SpriteRenderer>().color = new Color(Random.value,Random.value,Random.value);

        // Pasar el color al script de explosión
        /*Explosion explosionScript = enemy.GetComponent<Explosion>();
        if (explosionScript != null)
        {
            explosionScript.enemyColor = randomColor;
        }*/

        enemy.SetActive(true);
    }

    Vector3 GetRandomSpawnPosition(Vector3 enemySize)
    {
        float xPos = Random.Range(-screenMaxWidth / 2 + enemySize.x / 2, screenMaxWidth / 2 - enemySize.x / 2);
        float yPos = Random.Range(-screenMaxHeight / 2 + enemySize.y / 2, screenMaxHeight / 2 - enemySize.y / 2);
        return new Vector3(xPos, yPos, 0);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    private void RestartSpawning()
    {
        if (isSpawning)
        {
            StopSpawning();
            StartSpawning();
        }
    }
}
