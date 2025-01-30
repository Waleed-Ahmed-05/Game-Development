using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Include this for using legacy UI Text

public class Collision : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Respawn;
    public bool Key = false;
    private float timer = 120f; // Set timer for 120 seconds

    [SerializeField] private Text timerText; // Reference to legacy Text UI component

    void Update()
    {
        // Countdown timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            // Pause the game when the timer reaches zero
            timer = 0;
            UpdateTimerDisplay();
            Time.timeScale = 0.0f;
        }
    }

    private void UpdateTimerDisplay()
    {
        // Format the time as "Minutes:Seconds"
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Lava")
        {
            Player.transform.position = Respawn.transform.position;
        }
        else if (other.gameObject.tag == "Key")
        {
            Key = true;
        }
        else if (other.gameObject.tag == "Finish" && Key == true)
        {
            Time.timeScale = 0.0f;
        }
    }
}
