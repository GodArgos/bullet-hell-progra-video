using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Explosion explosionScript;

    // Serialized 
    [Header("Variables")]
    [Space(10)]
    public int playerHealth = 3;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timerShoot = 2f;

    // Non-Serialized
    private float timer;
    private bool canShoot = true;
    private Rigidbody2D rb;
    private DefaultControlls inputActions;
    private int instanceLifes;

    // Events
    [HideInInspector] public UnityEvent onHealthChanged;

    // UI
    [SerializeField] private GameObject lifeCountOverlay;

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
        explosionScript = GetComponent<Explosion>();

        instanceLifes = GameManager.Instance.playerLifes;
        playerHealth = instanceLifes;
    }

    void Update()
    {
        if(instanceLifes != GameManager.Instance.playerLifes)
        {
            instanceLifes = GameManager.Instance.playerLifes;
            playerHealth = instanceLifes;
        }

        if (!GameManager.Instance.playerHasDied && !GameManager.Instance.guiActivated)
        {
            Move();
            Rotate();
            CheckShootAble();
        } 
    }

    private void LateUpdate()
    {
        if (lifeCountOverlay != null)
        {
            lifeCountOverlay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("HP: {0000}", playerHealth);
        }
    }

    #endregion

    #region Movement
    private void Move()
    {
        Vector2 input = inputActions.Basic.Move.ReadValue<Vector2>();
        rb.velocity = input.normalized * GameManager.Instance.playerSpeed;
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
        if (canShoot && !GameManager.Instance.playerHasDied && !GameManager.Instance.guiActivated)
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

            if (explosionScript != null)
            {
                explosionScript.Explode(GetComponent<SpriteRenderer>().color);
            }
            // EXPLOSI�N

            StartCoroutine(WaitForRestart());
            
        }
        else
        {
            if (explosionScript != null)
            {
                explosionScript.Explode(GetComponent<SpriteRenderer>().color);
            }
            Destroy(gameObject);

            GameManager.Instance.FinalDeath();

        }
    }

    IEnumerator WaitForRestart()
    {
        GameManager.Instance.playerHasDied = true;
        rb.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.rotation = 0f;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(GameManager.Instance.explosionDuration + 0.5f);

        GetComponent<SpriteRenderer>().enabled = true;
        GameManager.Instance.playerHasDied = false;
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
