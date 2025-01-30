using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Chase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    [SerializeField] private float MaxHealth = 50;
    [SerializeField] private Healthbar Healthbar;
    private float CurrentHealth;

    private bool isPlayerInTrigger = false;
    private Player playerScript; // Reference to the Player script
    private float damageCooldown = 0.3f; // Time between damage applications
    private float lastDamageTime = 0f; // Tracks the last time damage was applied

    void Start()
    {
        CurrentHealth = MaxHealth;

        if (Healthbar != null)
        {
            Healthbar.UpdateHealhBar(MaxHealth, CurrentHealth);
        }
        else
        {
            Debug.LogError("Healthbar is not assigned in the Inspector.");
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on the Enemy GameObject.");
        }

        // Find the Player script
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
            if (playerScript == null)
            {
                Debug.LogError("Player script is missing on the Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (agent != null && player != null && !isPlayerInTrigger)
        {
            // Continue chasing if the player is not in the trigger zone
            agent.destination = player.position;
            agent.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            if (agent != null)
            {
                agent.isStopped = true; // Stop chasing the player
            }
            playerScript.TakeDamage(1);

            Debug.Log("Enemy entered trigger with Player.");

            // Check if the player is actively firing and apply initial damage
            if (playerScript != null && playerScript.isFiring && Time.time >= lastDamageTime + damageCooldown)
            {
                lastDamageTime = Time.time; // Update last damage time
                TakeDamage(5);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy is staying in trigger with Player.");

            if (agent != null)
            {
                agent.isStopped = true; // Stop chasing the player
            }

            // Check if the player is actively firing and apply damage at intervals
            if (playerScript != null && playerScript.isFiring && Time.time >= lastDamageTime + damageCooldown)
            {
                lastDamageTime = Time.time; // Update last damage time
                TakeDamage(2);
                playerScript.TakeDamage(1);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;

            if (agent != null)
            {
                agent.isStopped = false; // Resume chasing the player
            }

            Debug.Log("Enemy exited trigger with Player.");
        }
    }

    private void TakeDamage(float damage)
    {
        if (Healthbar == null)
        {
            Debug.LogWarning("Healthbar is null. Skipping health bar update.");
        }

        // Decrement health
        CurrentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {CurrentHealth}");

        // Update health bar
        if (Healthbar != null)
        {
            Healthbar.UpdateHealhBar(MaxHealth, CurrentHealth);
        }

        // Check if health is zero or below
        if (CurrentHealth <= 0)
        {
            SoundManager.Instance.PlayEnemyDieSound();

            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject); // Destroy the enemy
    }
}
