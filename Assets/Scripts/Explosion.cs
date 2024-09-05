using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Variables configurables
    public GameObject circlePrefab; // Prefab del circulo de la explosion
    public float explosionSpeed = 5f; // Velocidad a la que los circulos se alejan
    public float explosionRadius = 1f; // Radio inicial de la explosion
    public float explosionTime = 2f; // Tiempo que duran los circulos antes de destruirse
    public Color enemyColor;
    public float size = 0.5f;
    // Función que se llama cuando sucede la explosion

    private void Start()
    {
        enemyColor = Color.white;
    }

    public void Explode(Color color)
    {
        for (int i = 0; i < 8; i++)
        {
            // Calcular la posicion de los 8 circulos en la explosion
            float angle = i * Mathf.PI / 4; // Ángulos en radianes (45 grados entre cada circulo)
            Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * GameManager.Instance.explosionRadius;

            // Instanciar el circulo en la posición calculada
            GameObject explosionCircle = Instantiate(circlePrefab, spawnPos, Quaternion.identity);
            explosionCircle.transform.localScale = new Vector3 (size, size, size);
            explosionCircle.gameObject.GetComponent<SpriteRenderer>().color = color;
            
            // Aplicar fuerza para alejar el circulo desde el centro de la explosion
            Rigidbody2D rb = explosionCircle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * GameManager.Instance.explosionSpeed, ForceMode2D.Impulse);
            }

            // Destruir el circulo despues de explosionTime segundos
            Destroy(explosionCircle, GameManager.Instance.explosionDuration);
        }

        // Destruir el objeto que exploto (por ejemplo, un enemigo o jugador)
    }
}
