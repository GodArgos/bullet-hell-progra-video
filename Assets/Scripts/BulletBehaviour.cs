using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Variables")]
    [Space(10)]
    public float shootingSpeed = 2;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Agregar una kill al puntaje total

            //
            GameManager.Instance.killCount++;
            // Obtener el script de explosión del enemigo
            Explosion explosionScript = collision.gameObject.GetComponent<Explosion>();
            if (explosionScript != null)
            {
                Debug.Log("Llamado a explode");
                explosionScript.Explode(); // Llamar al método Explode
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
