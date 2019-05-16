using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Tooltip("The audio source that should play audio. Will default to the audio source on the object if no other audio source is specified.")]
    public AudioSource audioSource;
    public List<AudioMapping> audioMapping;

    public void PlayOneShotByName(string clipName)
    {
        audioSource.PlayOneShot(GetAudioClipByName(clipName));
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
