using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        inputActions = new DefaultControlls();
    }

    #endregion

    [Header("Global Variables")]
    [Space(10)]
    public float playerSpeed = 5f;
    public int playerLifes = 3;
    public float spawnTime = 5f;
    public float enemyRotation = 2f;
    public float explosionSpeed = 5f;
    public float explosionRadius = 1f;
    public float explosionDuration = 2f;
    public int killCount = 0;
    [HideInInspector] public bool playerHasDied = false;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject killCountOverlay;

    private float startTime = 0;
    private DefaultControlls inputActions;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
        inputActions.Basic.Restart.performed += ReStartGame;
        startTime = Time.time;
    }

    private void LateUpdate()
    {
        if (killCountOverlay != null)
        {
            killCountOverlay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("Kills: {0000}", killCount);
        }
    }

    private void ReStartGame(InputAction.CallbackContext action)
    {
        if (gameOverCanvas.activeSelf)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void FinalDeath()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            gameOverCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = killCount.ToString();
            
            float totalTime = Time.time - startTime;
            string mins = ((int)totalTime / 60).ToString("00");
            string segs = (totalTime % 60).ToString("00");
            string TimerString = string.Format("{00}:{01}", mins, segs);

            gameOverCanvas.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = TimerString;
            //Time.timeScale = 0f;
        }
    }

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
