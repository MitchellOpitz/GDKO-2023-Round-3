using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioTrack
{
    public AudioClip clip;
    public bool loop;
    public float volume = 1f;
}

public class AudioManager : MonoBehaviour
{
    public List<AudioTrack> tracks = new List<AudioTrack>();
    private AudioSource audioSource;

    private int currentTrackIndex = 0;

    private float fadeTime = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayCurrentTrack();
    }

    private void PlayCurrentTrack()
    {
        audioSource.clip = tracks[currentTrackIndex].clip;
        audioSource.loop = tracks[currentTrackIndex].loop;
        audioSource.volume = tracks[currentTrackIndex].volume;
        audioSource.Play();
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeOutCoroutine(time));
    }

    private IEnumerator FadeOutCoroutine(float time)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / time;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void ChangeTrack(int index)
    {
        if (index < 0 || index >= tracks.Count)
        {
            Debug.LogError("Invalid track index.");
            return;
        }

        currentTrackIndex = index;
        PlayCurrentTrack();
    }
}
