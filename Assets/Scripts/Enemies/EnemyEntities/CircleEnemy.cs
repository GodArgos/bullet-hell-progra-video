using UnityEngine;

public class CircleEnemy : Enemy {
    
    
    public override void Initialize(string name, float speed, float rotationSpeed, GameObject enemyClone)
    {
        name = name;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        enemyClone = enemyClone;
    }
}