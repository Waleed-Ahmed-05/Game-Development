using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For managing UI

public class Chapter2Manager : MonoBehaviour
{
    public GameObject checkpoint1;
    public GameObject checkpoint2;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public GameObject aura1;
    public GameObject aura2;
    public GameObject aura3;
    public GameObject aura4;

    public GameObject movingWall;
    public GameObject key1;
    public GameObject key2;

    public Transform player; // Player transform
    public GameObject respawnCanvas; // UI Canvas for respawn message
    public Text respawnText; // Text component for messages

    private Transform currentRespawnPosition;

    private bool key1CoroutineStarted = false;
    private bool key2CoroutineStarted = false;
    public GameObject portal; // Portal GameObject
    public GameObject portalLoad; // Portal loading effect GameObject
    void Start()
    {
        ValidateObjects();
        InitializeState();
    }

    void Update()
    {
        // Check for Room 1 enemies
        if (!key1CoroutineStarted && enemy1 == null && enemy2 == null)
        {
            key1CoroutineStarted = true;
            StartCoroutine(ActivateKeyWithDelay(key1));
        }

        // Check for Room 2 enemies
        if (!key2CoroutineStarted && enemy3 == null && enemy4 == null)
        {
            key2CoroutineStarted = true;
            StartCoroutine(ActivateKeyWithDelay(key2));
        }
        // Check for respawn input
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void StartRoom1Sequence()
    {
        StartCoroutine(Room1SequenceCoroutine());
    }

    public void StartRoom2Sequence()
    {
        StartCoroutine(Room2SequenceCoroutine());
    }

    private IEnumerator Room1SequenceCoroutine()
    {
        SoundManager.Instance.PlayCheckpointSound();
        if (checkpoint1 != null) checkpoint1.SetActive(false);

        yield return new WaitForSeconds(4f);

        if (checkpoint1 != null)
        {
            currentRespawnPosition = checkpoint1.transform;
            Debug.Log("Respawn position set to Checkpoint1.");
        }

        if (aura1 != null) aura1.SetActive(false);
        if (aura2 != null) aura2.SetActive(false);

        if (enemy1 != null)
        {
            SoundManager.Instance.PlayConjureSound();
            enemy1.SetActive(true);
            yield return new WaitForSeconds(4f);
        }

        if (enemy2 != null)
        {
            SoundManager.Instance.PlayConjureSound();
            enemy2.SetActive(true);
        }
    }

    private IEnumerator Room2SequenceCoroutine()
    {
        SoundManager.Instance.PlayCheckpointSound();
        if (checkpoint2 != null) checkpoint2.SetActive(false);

        yield return new WaitForSeconds(4f);

        if (checkpoint2 != null)
        {
            currentRespawnPosition = checkpoint2.transform;
            Debug.Log("Respawn position set to Checkpoint2.");
        }

        if (aura3 != null) aura3.SetActive(false);
        if (aura4 != null) aura4.SetActive(false);

        if (enemy3 != null)
        {
            SoundManager.Instance.PlayConjureSound();
            enemy3.SetActive(true);
            yield return new WaitForSeconds(4f);
        }

        if (enemy4 != null)
        {
            SoundManager.Instance.PlayConjureSound();
            enemy4.SetActive(true);
        }
    }

    private IEnumerator ActivateKeyWithDelay(GameObject key)
    {
        yield return new WaitForSeconds(2f); // Delay before activating the key

        if (key != null)
        {
            key.SetActive(true);
            SoundManager.Instance.PlayKeySound();
            Debug.Log($"{key.name} activated.");
        }
    }

    public void MoveWall()
    {
        if (movingWall != null)
        {
            SoundManager.Instance.PlayCameraMoveSound();
            StartCoroutine(MoveWallCoroutine());
        }
        else
        {
            Debug.LogError("MovingWall is not assigned.");
        }
    }

    private IEnumerator MoveWallCoroutine()
    {
        float targetY = 28f;
        float duration = 3.5f;
        float elapsedTime = 0f;

        Vector3 initialPosition = movingWall.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, targetY, initialPosition.z);

        Debug.Log("Starting wall movement...");

        while (elapsedTime < duration)
        {
            movingWall.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingWall.transform.position = targetPosition;
        Debug.Log("Wall reached target position. Destroying wall.");
        Destroy(movingWall);
    }

    private void ValidateObjects()
    {
        if (checkpoint1 == null) Debug.LogError("Checkpoint1 is not assigned.");
        if (checkpoint2 == null) Debug.LogError("Checkpoint2 is not assigned.");

        if (enemy1 == null) Debug.LogError("Enemy1 is not assigned.");
        if (enemy2 == null) Debug.LogError("Enemy2 is not assigned.");
        if (enemy3 == null) Debug.LogError("Enemy3 is not assigned.");
        if (enemy4 == null) Debug.LogError("Enemy4 is not assigned.");

        if (aura1 == null) Debug.LogError("Aura1 is not assigned.");
        if (aura2 == null) Debug.LogError("Aura2 is not assigned.");
        if (aura3 == null) Debug.LogError("Aura3 is not assigned.");
        if (aura4 == null) Debug.LogError("Aura4 is not assigned.");

        if (movingWall == null) Debug.LogError("MovingWall is not assigned.");
        if (key1 == null) Debug.LogError("Key1 is not assigned.");
        if (key2 == null) Debug.LogError("Key2 is not assigned.");
    }

    private void InitializeState()
    {
        if (enemy1 != null) enemy1.SetActive(false);
        if (enemy2 != null) enemy2.SetActive(false);
        if (enemy3 != null) enemy3.SetActive(false);
        if (enemy4 != null) enemy4.SetActive(false);

        if (aura1 != null) aura1.SetActive(false);
        if (aura2 != null) aura2.SetActive(false);
        if (aura3 != null) aura3.SetActive(false);
        if (aura4 != null) aura4.SetActive(false);

        if (key1 != null) key1.SetActive(false);
        if (key2 != null) key2.SetActive(false);

        currentRespawnPosition = null;

        Debug.Log("Scene initialized to default state.");
    }

    public void Respawn()
    {
        if (player == null || respawnText == null || respawnCanvas == null)
        {
            Debug.LogError("Player or Respawn UI is not assigned.");
            return;
        }

        // respawnCanvas.SetActive(true); // Show respawn message

        if (currentRespawnPosition != null &&
            (currentRespawnPosition.position == checkpoint1.transform.position ||
             currentRespawnPosition.position == checkpoint2.transform.position))
        {
            // Respawn at the current position
            player.position = currentRespawnPosition.position;
            respawnText.text = "Respawning at the last checkpoint...";
            Debug.Log("Respawning player at checkpoint.");
        }
        else
        {
            // Restart the scene
            respawnText.text = "No valid checkpoint found. Restarting the scene...";
            Debug.Log("Restarting the scene...");
            StartCoroutine(RestartSceneAfterDelay(0f));
        }

        StartCoroutine(HideRespawnCanvasAfterDelay(1f)); // Hide the canvas after 2 seconds


    }

    private IEnumerator HideRespawnCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (respawnCanvas != null)
        {
            respawnCanvas.SetActive(false); // Hide the canvas
        }
    }

    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
    }

    public void ActivatePortal()
    {
        StartCoroutine(ActivatePortalCoroutine());
    }

    private IEnumerator ActivatePortalCoroutine()
    {
        SoundManager.Instance.PlayPortalSound();


        if (portalLoad != null)
        {
            portalLoad.SetActive(true); // Activate portal load effect
            Debug.Log("Portal load activated.");
        }

        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        portalLoad.SetActive(false); // Activate portal load effect


        if (portal != null)
        {
            portal.SetActive(true); // Activate portal
            Debug.Log("Portal activated.");
        }
    }

}
