using UnityEngine;
using System.Collections;

public class VerticalMovement : MonoBehaviour
{
    public float yMin = 0f;  // Minimum Y value
    public float yMax = 5f;  // Maximum Y value
    public float speed = 2f; // Speed of oscillation
    public float waitTimeAtExtremes = 2f; // Time to wait at each extreme (in seconds)

    private Vector3 initialPosition;
    private bool isWaiting = false;

    void Start()
    {
        // Store the object's initial position
        initialPosition = transform.position;

        // Start the oscillation coroutine
        StartCoroutine(OscillateVertical());
    }

    IEnumerator OscillateVertical()
    {
        while (true)
        {
            // Move up from yMin to yMax
            yield return StartCoroutine(MoveToPosition(yMax));

            // Wait at the top for specified time
            yield return new WaitForSeconds(waitTimeAtExtremes);

            // Move down from yMax to yMin
            yield return StartCoroutine(MoveToPosition(yMin));

            // Wait at the bottom for specified time
            yield return new WaitForSeconds(waitTimeAtExtremes);
        }
    }

    // Coroutine to move the object to a target position with smooth interpolation
    IEnumerator MoveToPosition(float targetY)
    {
        float startY = transform.position.y;
        float elapsedTime = 0f;

        // Smoothly move to the target Y position
        while (elapsedTime < (Mathf.Abs(targetY - startY) / speed))
        {
            float newY = Mathf.Lerp(startY, targetY, (elapsedTime * speed) / Mathf.Abs(targetY - startY));
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we reach the exact target position after the loop
        transform.position = new Vector3(initialPosition.x, targetY, initialPosition.z);
    }
}
