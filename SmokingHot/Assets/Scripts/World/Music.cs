using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int lastPlayedIndex = -1; // Keep track of the last played clip

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the GameObject!");
            return;
        }

        PlayRandomClip();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomClip();
        }
    }

    private void PlayRandomClip()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned to the audioClips array!");
            return;
        }

        int newIndex;
        do
        {
            newIndex = Random.Range(0, audioClips.Length);
        }
        while (newIndex == lastPlayedIndex); // Ensure it's not the same as the last clip

        lastPlayedIndex = newIndex;

        audioSource.clip = audioClips[newIndex];
        //audioSource.Play();
    }
}
