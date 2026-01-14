using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenFragmentEnemy : MonoBehaviour
{
public float speed;
    Rigidbody2D rb;
    Vector3 LastVelocity;

    // Saved variables when player dies
    private Vector2 savedVelocity;
    private bool alreadyStop = false;
    private float rot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rot = transform.rotation.z;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (!GameManager.Instance.playerHasDied)
        {
            if (rb.velocity == Vector2.zero)
            {
                LastVelocity = savedVelocity;
                rb.velocity = savedVelocity;
            }
            else
            {
                LastVelocity = rb.velocity;
            }
        }
        else
        {
            if (!alreadyStop)
            {
                savedVelocity = rb.velocity;
                alreadyStop = true;
            }
            
            rb.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().onHealthChanged.Invoke();
        } 
    }

    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }
}
