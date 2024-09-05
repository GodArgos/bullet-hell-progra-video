using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Vector3 LastVelocity;

    // Saved variables when player dies
    private Vector2 savedVelocity;
    private bool alreadyStop = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(20 * speed, 20 * speed));
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

            rb.rotation += GameManager.Instance.enemyRotation;
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
