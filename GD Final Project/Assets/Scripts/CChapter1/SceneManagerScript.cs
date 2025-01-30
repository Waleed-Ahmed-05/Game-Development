using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    public float moveSpeed = 1f;  // Speed of camera movement
    public float panelDisplayTime = 5f; // Time to display the panel
    public GameObject panel;      // Reference to the UI panel to show

    private Camera mainCamera;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private SoundManager soundManager;  // Reference to the SoundManager

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
        initialPosition = new Vector3(mainCamera.transform.position.x, 10f, 123f);  // Initial position
        targetPosition = new Vector3(mainCamera.transform.position.x, 8f, 136f);  // Target position
        
        // Hide the panel at the start
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Get the SoundManager component
        soundManager = FindObjectOfType<SoundManager>();  // Make sure the SoundManager exists in the scene
    }

    public void PlayAction()
    {
        // Start the sequence of actions
        StartCoroutine(ExecuteSequence());
    }

    private IEnumerator ExecuteSequence()
    {
        // Play camera move sound when the camera starts moving
        if (soundManager != null)
        {
            soundManager.PlayCameraMoveSound();
        }

        // Move the camera smoothly from initialPosition to targetPosition
        float timeElapsed = 0f;
        while (timeElapsed < moveSpeed)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / moveSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera ends exactly at targetPosition
        mainCamera.transform.position = targetPosition;

        // Pause for 1 second before showing the panel
        yield return new WaitForSeconds(1f);

        // Play message sound when the panel is shown
        if (soundManager != null && panel != null)
        {
            soundManager.PlayMessageSound();
        }

        // Show the panel for the specified time
        if (panel != null)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(panelDisplayTime);
            panel.SetActive(false);
        }

        // Play camera move sound when switching scenes (to indicate the end of the current sequence)
        if (soundManager != null)
        {
            soundManager.PlayCameraMoveSound();
        }

        // Transition to the next scene (replace "NextScene" with your scene name)
        SceneManager.LoadScene("Chapter 2");
    }
}
