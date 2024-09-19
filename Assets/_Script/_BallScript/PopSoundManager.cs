using UnityEngine;

public class PopSoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] popSounds;   // Array to hold the pop sound clips
    public AudioClip winClip;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensures this object persists across scenes

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is missing!");
                return;
            }
        }

        if (popSounds.Length < 3)
        {
            Debug.LogWarning("Please assign at least 3 pop sounds to the array.");
        }
    }
    void Start()
    {
        Debug.Log("Game Started. AudioSource status: " + (audioSource != null ? "Assigned" : "Missing"));

        if (popSounds.Length > 0)
        {
            for (int i = 0; i < popSounds.Length; i++)
            {
                Debug.Log("Pop sound " + i + ": " + (popSounds[i] != null ? "Assigned" : "Missing"));
            }
        }
    }



    // Call this method to play a random pop sound
    public void PlayRandomPopSound()
    {
        if (popSounds == null || popSounds.Length == 0)
        {
            Debug.LogWarning("Pop sounds array is empty or unassigned!");
            return;
        }

        // Select a random pop sound from the array
        int randomIndex = Random.Range(0, popSounds.Length);
        AudioClip selectedClip = popSounds[randomIndex];

        // Check if the selected audio clip and audio source are valid
        if (selectedClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(selectedClip);
        }
        else
        {
            Debug.LogWarning("Cannot play sound. Either the AudioSource or AudioClip is missing!");
        }
    }

    // Optionally, if you need to stop playing any currently playing audio:
    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(winClip);
    }
}
