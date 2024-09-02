using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private bool leaveCollision = true;
    Rigidbody2D rb;
    Vector3 LastVelocity;

    private Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
        rb.AddForce(new Vector2(20 * speed, 20 * speed));
    }

    void Update()
    {
        LastVelocity = rb.velocity;
        //Move();
        // CheckBounds();
    }

    void Move()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    void CheckBounds()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 position = transform.position;

        if (position.x < -screenBounds.x || position.x > screenBounds.x)
        {
            velocity.x = -velocity.x;
        }
        if (position.y < -screenBounds.y || position.y > screenBounds.y)
        {
            velocity.y = -velocity.y;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("LEAVE COLLISION ENTER: "+leaveCollision);
        if (other.gameObject.CompareTag("Bound"))
        {
            var speed = LastVelocity.magnitude;
            var direction = Vector3.Reflect(LastVelocity.normalized, other.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
            //CheckBounds();
        }
    }
}
