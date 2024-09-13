using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            // Obtener el script de explosi�n del enemigo

            if (collision.gameObject.TryGetComponent(out CircleEnemy scriptC))
            {
                scriptC.EnemyDestroy();
            }
            else if(collision.gameObject.TryGetComponent(out DiamondEnemy scriptD))
            {
                scriptD.EnemyDestroy();
            }
            else if (collision.gameObject.TryGetComponent(out CylinderEnemy scriptCy))
            {
                scriptCy.EnemyDestroy();
            }
            else if (collision.gameObject.TryGetComponent(out BossEnemy scriptB))
            {
                scriptB.EnemyDestroy();
            }
            else
            {
                collision.gameObject.GetComponent<ShurikenFragmentEnemy>().EnemyDestroy();
            }

            Destroy(gameObject);
        }
    }
}
