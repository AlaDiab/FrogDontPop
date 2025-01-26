using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        AudioClip sound = Resources.Load<AudioClip>($"Audio/{fileNames[selection]}");
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
        AudioClip sound = Resources.Load<AudioClip>($"Audio/{fileNames[selection]}");
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

    public static void Play(bool loop, int soundIndex, int channel)
    {
        // Ensure the GameManager is available
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not available.");
            return;
        }

        // Find audio source
        AudioSource audioSource = null;
        switch (channel)
        {
            case 0:
                audioSource = UnityEngine.Object.FindObjectOfType<Camera>().GetComponent<AudioSource>();
                break;
            case 1:
                audioSource = GameManager.Instance.GetComponent<AudioSource>();
                break;
            case 2:
                audioSource = UnityEngine.Object.FindObjectOfType<Player>().GetComponent<AudioSource>();
                break;
        }

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found in the scene.");
            return;
        }

        // Load the sound from GameManager
        if (soundIndex < 0 || soundIndex >= GameManager.Instance.sounds.Length)
        {
            Debug.LogError($"Sound index {soundIndex} is out of range.");
            return;
        }

        AudioClip sound = GameManager.Instance.sounds[soundIndex];
        if (sound == null)
        {
            Debug.LogError($"AudioClip at index {soundIndex} is null.");
            return;
        }

        // Play the sound
        audioSource.loop = loop;
        audioSource.clip = sound;
        audioSource.Play();
    }
}
