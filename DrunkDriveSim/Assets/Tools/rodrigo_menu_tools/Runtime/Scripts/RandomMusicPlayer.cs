
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    [Header("List of music tracks")]
    public AudioClip[] tracks;

    [Header("Audio Source")]
    public AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayRandomTrack();
    }

    private void Update()
    {
        // When the track finishes, play another one
        if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        if (tracks.Length == 0)
        {
            Debug.LogWarning("RandomMusicPlayer: No tracks assigned!");
            return;
        }

        // Pick a random clip
        AudioClip clip = tracks[Random.Range(0, tracks.Length)];

        audioSource.clip = clip;
        audioSource.Play();
    }
}
