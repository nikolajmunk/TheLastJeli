using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [System.Serializable]
    public class AudioMapping
    {
        public string name;
        public AudioClip audioClip;
    }

    [Tooltip("The audio source that should play audio. Will default to the audio source on the object if no other audio source is specified.")]
    public AudioSource audioSource;
    public List<AudioMapping> audioMapping;

    public void PlayOneShotByName(string clipName, bool pickRandomClipWithName = false)
    {
        if (pickRandomClipWithName)
        {
            audioSource.PlayOneShot(GetRandomAudioClipByName(clipName));
        }
        else
        {
            audioSource.PlayOneShot(GetAudioClipByName(clipName));
        }
    }

    public void PlayOneShotWithRandomPitch(string clipName, float minPitch = 1, float maxPitch = 1, bool pickRandomClipWithName = false)
    {
        float previousPitch = audioSource.pitch;

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        PlayOneShotByName(clipName, pickRandomClipWithName);

    }

    public AudioClip GetAudioClipByName(string name)
    {
        foreach (AudioMapping mapping in audioMapping)
        {
            if (mapping.name == name)
            {
                return mapping.audioClip;
            }
        }
        return null;
    }

    public AudioClip GetRandomAudioClipByName(string name)
    {
        List<AudioClip> clipsToPlay = new List<AudioClip>();
        foreach (AudioMapping mapping in audioMapping)
        {
            if (mapping.name == name)
            {
                clipsToPlay.Add(mapping.audioClip);
            }
        }
        int numberOfClips = clipsToPlay.Count;
        if (numberOfClips > 0)
        {
            return clipsToPlay[Random.Range(0, numberOfClips)];
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
