using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator animator;
    public GameObject slashEffect;
    public bool isFiring; // Tracks if the player is actively firing
    public AudioSource fireSound; // Reference to the AudioSource for the fire sound effect

    private Chapter2Manager chapterManager; // Reference to the Chapter2Manager

    // New Fields
    public GameObject gameWinText; // Game Win Text GameObject
    public GameObject gameOverText; // Game Over Text GameObject
    public Text healthText; // UI Text for displaying health
    public int maxHealth = 500; // Maximum health
    private int currentHealth; // Current health

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Ensure necessary components
        EnsureCollisionComponents();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component is missing from the Player GameObject.");
            }
        }

        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
        }
        else
        {
            Debug.LogError("SlashEffect GameObject is not assigned.");
        }

        if (fireSound == null)
        {
            fireSound = GetComponent<AudioSource>();
            if (fireSound == null)
            {
                Debug.LogError("AudioSource component is missing from the Player GameObject.");
            }
        }

        // Find the Chapter2Manager in the scene
        chapterManager = FindObjectOfType<Chapter2Manager>();
        if (chapterManager == null)
        {
            Debug.LogError("Chapter2Manager is missing in the scene.");
        }

        isFiring = false; // Initialize as not firing

        // Hide game win and game over texts initially
        if (gameWinText != null) gameWinText.SetActive(false);
        if (gameOverText != null) gameOverText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFiring) // Block firing if already active
        {
            TriggerFireAnimation();
        }
    }

    void EnsureCollisionComponents()
    {
        if (!HasComponentInChildren<Collider>())
        {
            Debug.LogError("No Collider component found on the Player or its children. Collisions will not work.");
        }

        if (!HasComponentInChildren<Rigidbody>())
        {
            Debug.LogError("No Rigidbody component found on the Player or its children. Collisions may not work.");
        }
    }

    bool HasComponentInChildren<T>() where T : Component
    {
        return GetComponentInChildren<T>() != null;
    }

    void TriggerFireAnimation()
    {
        if (animator != null)
        {
            isFiring = true; // Set firing to true
            animator.SetBool("fire", true);
            Debug.Log("Fire animation triggered.");

            // Play the fire sound effect
            if (fireSound != null)
            {
                fireSound.Play();
                Debug.Log("Fire sound effect played.");
            }

            if (slashEffect != null)
            {
                Invoke("ActivateSlashEffect", 0.2f);
            }

            Invoke("ResetFireAnimation", 0.5f);
        }
    }

    void ActivateSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.SetActive(true);
            Debug.Log("Slash effect activated.");
        }
    }

    void ResetFireAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("fire", false);
        }

        isFiring = false; // Reset firing to false
        Debug.Log("Fire animation reset.");

        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
            Debug.Log("Slash effect deactivated.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint1"))
        {
            Debug.Log("Player entered Checkpoint1. Triggering Room 1 Sequence.");

            if (chapterManager != null)
            {
                chapterManager.StartRoom1Sequence();
            }
        }
        else if (other.CompareTag("Checkpoint2"))
        {
            Debug.Log("Player entered Checkpoint2. Triggering Room 2 Sequence.");

            if (chapterManager != null)
            {
                chapterManager.StartRoom2Sequence();
            }
        }
        else if (other.CompareTag("Key1"))
        {
            SoundManager.Instance.PlayCheckpointSound();
            Debug.Log("Player entered a trigger with a Key.");
            HandleKeyTrigger(other.gameObject);
        }
        else if (other.CompareTag("Key2"))
        {
            SoundManager.Instance.PlayCheckpointSound();
            chapterManager.ActivatePortal();
            Debug.Log("Player entered a trigger with a Key2.");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Death"))
        {
            chapterManager.Respawn();
        }
        else if (other.CompareTag("Key"))
        {
            WinGame();
        }
    }

    void HandleKeyTrigger(GameObject key)
    {
        Destroy(key);
        chapterManager.MoveWall();
        Debug.Log("Key1 collected and destroyed.");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
        chapterManager.Respawn();


        // Optionally, trigger respawn or end the game
        gameOverText.SetActive(false);

    }

    public void WinGame()
    {
        Debug.Log("Player has won the game!");

        if (gameWinText != null)
        {
            gameWinText.SetActive(true);
        }

        // Optionally, end the game or load a new scene
    }
}
