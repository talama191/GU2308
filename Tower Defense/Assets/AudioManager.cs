using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip buttonSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.clip = buttonSound;
            _audioSource.Play();
        }
    }
}
