using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string name;
    public float speed;
    public float rotationSpeed;
    public GameObject enemyClone;

    public abstract void Initialize(string name, float speed, float rotationSpeed, GameObject enemyClone);
}
