using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    public float speed = 5f;          // Speed of movement
    public float moveDistance = 5f;   // Distance to move horizontally

    private Vector3 startPosition;
    private bool movingRight = true;  // Track movement direction

    void Start()
    {
        // Record the starting position of the enemy
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate target position based on direction
        float targetX = movingRight ? startPosition.x + moveDistance : startPosition.x - moveDistance;

        // Move the enemy towards the target position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, startPosition.y, startPosition.z), speed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (transform.position.x == targetX)
        {
            movingRight = !movingRight; // Change direction
        }
    }
}
