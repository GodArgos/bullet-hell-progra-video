using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Variables configurables
    public GameObject circlePrefab; // Prefab del circulo de la explosion
    public float explosionSpeed = 5f; // Velocidad a la que los circulos se alejan
    public float explosionRadius = 1f; // Radio inicial de la explosion
    public float explosionTime = 2f; // Tiempo que duran los circulos antes de destruirse
    public Color enemyColor;
    // Función que se llama cuando sucede la explosion

    public void Explode()
    {
        for (int i = 0; i < 8; i++)
        {
            // Calcular la posicion de los 8 circulos en la explosion
            float angle = i * Mathf.PI / 4; // Ángulos en radianes (45 grados entre cada circulo)
            Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * explosionRadius;

            // Instanciar el circulo en la posición calculada
            GameObject explosionCircle = Instantiate(circlePrefab, spawnPos, Quaternion.identity);

            
            // Aplicar fuerza para alejar el circulo desde el centro de la explosion
            Rigidbody2D rb = explosionCircle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * explosionSpeed, ForceMode2D.Impulse);
            }

            // Destruir el circulo despues de explosionTime segundos
            Destroy(explosionCircle, explosionTime);
        }

        // Destruir el objeto que exploto (por ejemplo, un enemigo o jugador)
    }
}
