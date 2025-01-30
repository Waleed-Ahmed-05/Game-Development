using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance for global access

    // Public variables to assign audio clips in the Inspector
    public AudioClip buttonClickClip;
    public AudioClip cameraMoveClip;
    public AudioClip messageClip;
    public AudioClip backgroundMusicClip;
    public AudioClip buttonHoverClip;

    // New sounds for your game
    public AudioClip enemyDieClip;
    public AudioClip conjureClip;
    public AudioClip checkpointClip;
    public AudioClip portalClip;
    public AudioClip keyClip;

    private AudioSource effectAudioSource; // For sound effects
    private AudioSource backgroundAudioSource; // For background music

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure persistence across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        // Initialize AudioSources
        effectAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();

        // Setup background music if assigned
        if (backgroundMusicClip != null)
        {
            backgroundAudioSource.clip = backgroundMusicClip;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }

    // Function to play button click sound
    public void PlayButtonClickSound()
    {
        PlaySound(buttonClickClip);
    }

    // Function to play camera move sound
    public void PlayCameraMoveSound()
    {
        PlaySound(cameraMoveClip);
    }

    // Function to play message sound
    public void PlayMessageSound()
    {
        PlaySound(messageClip);
    }

    // Function to play button hover sound
    public void PlayButtonHoverSound()
    {
        PlaySound(buttonHoverClip);
    }

    // Function to play enemy die sound
    public void PlayEnemyDieSound()
    {
        PlaySound(enemyDieClip);
    }

    // Function to play conjure sound
    public void PlayConjureSound()
    {
        PlaySound(conjureClip);
    }

    // Function to play checkpoint sound
    public void PlayCheckpointSound()
    {
        PlaySound(checkpointClip);
    }

    // Function to play portal sound
    public void PlayPortalSound()
    {
        PlaySound(portalClip);
    }

    // Function to play key sound
    public void PlayKeySound()
    {
        PlaySound(keyClip);
    }

    // Generic function to play a sound
    private void PlaySound(AudioClip clip)
    {
        if (effectAudioSource != null && clip != null)
        {
            effectAudioSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning("AudioClip is missing or not assigned.");
        }
        else
        {
            Debug.LogWarning("EffectAudioSource is not initialized.");
        }
    }

    // Function to change background music
    public void ChangeBackgroundMusic(AudioClip newMusic)
    {
        if (backgroundAudioSource != null && newMusic != null)
        {
            backgroundAudioSource.clip = newMusic;
            backgroundAudioSource.Play();
        }
        else if (newMusic == null)
        {
            Debug.LogWarning("New background music AudioClip is missing.");
        }
    }

    // Function to stop background music
    public void StopBackgroundMusic()
    {
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Stop();
        }
        else
        {
            Debug.LogWarning("BackgroundAudioSource is not initialized.");
        }
    }

    // Optional: Function to adjust background music volume dynamically
    public void SetBackgroundMusicVolume(float volume)
    {
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.volume = Mathf.Clamp01(volume); // Ensure volume is between 0 and 1
        }
        else
        {
            Debug.LogWarning("BackgroundAudioSource is not initialized.");
        }
    }
}
