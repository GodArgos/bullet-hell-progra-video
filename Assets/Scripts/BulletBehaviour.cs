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
            if (collision.gameObject.TryGetComponent(out CircleEnemy scriptC))
            {
                scriptC.EnemyDestroy();
                GameManager.Instance.killCount++;
            }
            else if(collision.gameObject.TryGetComponent(out DiamondEnemy scriptD))
            {
                scriptD.EnemyDestroy();
            }
            else if (collision.gameObject.TryGetComponent(out CylinderEnemy scriptCy))
            {
                scriptCy.EnemyDestroy();
                GameManager.Instance.killCount++;
            }
            else if (collision.gameObject.TryGetComponent(out BossEnemy scriptB))
            {
                scriptB.EnemyDestroy();
                GameManager.Instance.killCount += 2;
            }
            else
            {
                collision.gameObject.GetComponent<ShurikenFragmentEnemy>().EnemyDestroy();
            }

            Destroy(gameObject);
        }
    }
}
