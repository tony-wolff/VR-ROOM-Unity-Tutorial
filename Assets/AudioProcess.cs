using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcess : MonoBehaviour
{
    public AudioClip onSuccess;
    public AudioClip onFail;
    AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioSucess()
    {
        audio_source.clip = onSuccess;
        audio_source.Play();
    }

    public void PlayAudioFailure()
    {
        audio_source.clip = onFail;
        audio_source.Play();
    }
}
