using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
