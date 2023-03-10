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
        audioSource.Play();
        FadeIn(1f);
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
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / time;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    public void ChangeTrack(int index, float fadeTime)
    {
        if (index < 0 || index >= tracks.Count)
        {
            Debug.LogError("Invalid track index.");
            return;
        }

        if (currentTrackIndex == index)
        {
            return;
        }

        StartCoroutine(ChangeTrackCoroutine(index, fadeTime));
    }

    private IEnumerator ChangeTrackCoroutine(int index, float fadeTime)
    {
        if (fadeTime > 0f)
        {
            // Fade out the current track
            StartCoroutine(FadeOutCoroutine(fadeTime));
            yield return new WaitForSeconds(fadeTime);
        }

        // Stop the current track and play the new one
        audioSource.Stop();
        audioSource.ignoreListenerPause = true;
        currentTrackIndex = index;
        PlayCurrentTrack();

        if (fadeTime > 0f)
        {
            // Fade in the new track
            StartCoroutine(FadeInCoroutine(fadeTime));
        }
    }



    public void FadeIn(float time)
    {
        StartCoroutine(FadeInCoroutine(time));
    }

    private IEnumerator FadeInCoroutine(float time)
    {

        while (audioSource.volume < 1)
        {
            audioSource.volume += 1 * Time.unscaledDeltaTime / time;

            yield return null;
        }
    }
}
