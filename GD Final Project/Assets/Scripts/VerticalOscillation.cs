using UnityEngine;

public class VerticalOscillation : MonoBehaviour
{
    public float yMin = 0f;  // Minimum Y value
    public float yMax = 5f;  // Maximum Y value
    public float speed = 2f; // Speed of oscillation

    private Vector3 initialPosition;

    void Start()
    {
        // Store the object's initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the oscillated Y value between yMin and yMax based on time and speed
        float newY = Mathf.PingPong(Time.time * speed, yMax - yMin) + yMin;

        // Update the object's position (keep X and Z, but update Y)
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
