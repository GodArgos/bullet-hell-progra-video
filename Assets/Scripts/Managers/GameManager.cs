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
    public int killCount = 0;
    public float spawnTime = 5f;
    public float enemyRotation = 2f;
}
