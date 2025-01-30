using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float speed = 5f;        // Speed of movement
    public float amplitude = 3f;    // The maximum distance to move back and forth

    private Vector3  startPosition;  // Starting position of the object

    void Start()
    {
        // Store the object's initial position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate horizontal oscillation based on time
        float horizontalMovement = Mathf.Sin(Time.time * speed) * amplitude;

        // Update the object's position
        transform.position = new Vector3(startPosition.x + horizontalMovement, transform.position.y, transform.position.z);
    }
}
