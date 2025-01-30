using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;        // Speed of movement
    public float moveDistance = 3f; // Distance to move up and down

    private Vector3 startPosition;
    private bool movingUp = true;   // Track movement direction

    void Start()
    {
        // Record the starting position of the enemy
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate target position based on direction
        float targetY = movingUp ? startPosition.y + moveDistance : startPosition.y - moveDistance;

        // Move the enemy towards the target position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPosition.x, targetY, startPosition.z), speed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (transform.position.y == targetY)
        {
            movingUp = !movingUp; // Change direction
        }
    }
}
