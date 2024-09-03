using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [Space(10)]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Space(15)]

    // Serialized 
    [Header("Variables")]
    [Space(10)]
    public int playerHealth = 3;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timerShoot = 2f;

    // Non-Serialized
    private float timer;
    private bool canShoot = true;
    private Rigidbody2D rb;
    private DefaultControlls inputActions;

    // Events
    public UnityEvent onHealthChanged;

    #region General Event Methods
    private void Awake()
    {
        inputActions = new DefaultControlls();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = timerShoot;
        inputActions.Basic.Fire.performed += Shoot;
        onHealthChanged.AddListener(CheckHealth);
    }

    void Update()
    {
        Move();
        Rotate();
        CheckShootAble();
    }

    #endregion

    #region Movement
    private void Move()
    {
        Vector2 input = inputActions.Basic.Move.ReadValue<Vector2>();
        rb.velocity = input.normalized * playerSpeed;
    }   

    private void Rotate()
    {
        Vector2 mousePos = inputActions.Basic.MousePosition.ReadValue<Vector2>();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(mousePos) - transform.position);
        shootPoint.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(mousePos) - transform.position);
    }

    #endregion

    #region Abilities
    private void Shoot(InputAction.CallbackContext action)
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = shootPoint.position;
            bullet.GetComponent<Rigidbody2D>().velocity = shootPoint.up * bullet.GetComponent<BulletBehaviour>().shootingSpeed;

            canShoot = false;
        }
    }

    public void CheckShootAble()
    {
        if (!canShoot)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = timerShoot;
                canShoot = true;
            }
        }
    }
    #endregion

    #region Health Logic
    public void CheckHealth()
    {
        if (playerHealth > 1)
        {
            playerHealth--;

            // EXPLOSIÓN

            //
            StartCoroutine(WaitForRestart());
            transform.position = new Vector3(0f, 0f, 0f);
        }
        else
        {
            Destroy(gameObject);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        }
    }

    IEnumerator WaitForRestart()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }
      
    #endregion

    #region Input System Methods
    private void OnEnable()
    {
        inputActions.Basic.Enable();
    }

    private void OnDisable()
    {
        inputActions.Basic.Disable();
    }
    #endregion
}
