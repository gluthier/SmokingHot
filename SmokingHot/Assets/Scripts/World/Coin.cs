using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip[] soundClips; // Array to hold sound clips
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure there is an AudioSource component attached
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (soundClips.Length > 0)
        {
            // Pick a random sound clip from the array
            AudioClip randomClip = soundClips[Random.Range(0, soundClips.Length)];
            audioSource.PlayOneShot(randomClip);
        }
        else
        {
            Debug.LogWarning("No sound clips assigned to the soundClips array!");
        }
    }
}
