using UnityEngine;

public static class AudioManager
{
    public static void Play(params string[] fileNames)
    {
        // Find audio source
        AudioSource audioSource = UnityEngine.Object.FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found in the scene.");
            return;
        }

        // Select and load audio clip to play
        int selection = Random.Range(0, fileNames.Length);
        AudioClip sound = Resources.Load<AudioClip>($"Sounds/SFX/{fileNames[selection]}");
        if (sound == null)
        {
            Debug.LogError($"AudioClip '{fileNames[selection]}' not found in Resources/Audio.");
            return;
        }

        // Play the audio clip
        audioSource.clip = sound;
        audioSource.Play();
    }

    public static void Play(bool loop, params string[] fileNames)
    {
        // Find audio source
        AudioSource audioSource = UnityEngine.Object.FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found in the scene.");
            return;
        }

        // Select and load audio clip to play
        int selection = Random.Range(0, fileNames.Length);
        AudioClip sound = Resources.Load<AudioClip>($"Sounds/SFX/{fileNames[selection]}");
        if (sound == null)
        {
            Debug.LogError($"AudioClip '{fileNames[selection]}' not found in Resources/Audio.");
            return;
        }

        // Set volume and play the audio clip
        audioSource.loop = loop;
        audioSource.clip = sound;
        audioSource.Play();
    }
}
