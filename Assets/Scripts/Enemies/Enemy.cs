using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Enemy : MonoBehaviour
{
    public string Name;
    public float speed;
    public float rotationSpeed;
    public GameObject enemyClone;

    Rigidbody2D rb;
    Vector3 LastVelocity;

    // Saved variables when player dies
    private Vector2 savedVelocity;
    private bool alreadyStop = false;

    void Start()
    {
        transform.name = Name;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(20 * speed, 20 * speed));
    }


    void Update()
    {   
        if (!GameManager.Instance.playerHasDied)
        {
            if (rotationSpeed != GameManager.Instance.enemyRotation)
            {
                rotationSpeed = GameManager.Instance.enemyRotation;
            }

            if (alreadyStop) { alreadyStop = false; }
            
            if (rb.velocity == Vector2.zero)
            {
                LastVelocity = savedVelocity;
                rb.velocity = savedVelocity;
            }
            else
            {
                LastVelocity = rb.velocity;
            }

            rb.rotation += rotationSpeed;
        }
        else
        {
            if (!alreadyStop)
            {
                savedVelocity = rb.velocity;
                alreadyStop = true;
            }

            rb.velocity = Vector2.zero;
            rb.rotation = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().onHealthChanged.Invoke();
        }
        else
        {
            var speed = LastVelocity.magnitude;
            var direction = Vector3.Reflect(LastVelocity.normalized, other.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    public void EnemyDestroy()
    {
        GetComponent<Explosion>().Explode(GetComponent<SpriteRenderer>().color);
        Destroy(gameObject);
    }
}
