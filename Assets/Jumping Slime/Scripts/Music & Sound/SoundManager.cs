using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _backgroundMusicAudioSource;
    [SerializeField] private AudioSource _playerAudioSource, _objectsAudioSource, _coinAudioSource, _brownPlaformAudioSource;

    private AudioSource[] _generalSoundsAudioSources = new AudioSource[4];
    public AudioSource BackgroundMusicAudioSource => _backgroundMusicAudioSource;
    public AudioSource[] GeneralSoundsAudioSources => _generalSoundsAudioSources;

    [SerializeField] private AudioClip[] _objectsSounds;

    public void Initialize()
    {
        Debug.Log("Sound Manager Initialize");

        if (Instance != null)
        {
            throw new UnityException("One Date Manager Instance only!");
        }
        Instance = this;
        DontDestroyOnLoad(this);

        Debug.Log("Sound Manager Initilized");

        _backgroundMusicAudioSource.Play();
        _generalSoundsAudioSources[0] = _playerAudioSource;
        _generalSoundsAudioSources[1] = _objectsAudioSource;
        _generalSoundsAudioSources[2] = _coinAudioSource;
        _generalSoundsAudioSources[3] = _brownPlaformAudioSource;
    }

    private void PlayPlayerSound(AudioClip sound)
    {
        _playerAudioSource.clip = sound;
        _playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
        _playerAudioSource.Play();
    }

    public void PlayObjectSound(int objectIndex)
    {
        if (objectIndex == 0) // если звук слайма то в отдельную аудиосурс, чтобы слайм не прерывал звук объекта 
        {
            PlayPlayerSound(_objectsSounds[objectIndex]);
        }
        else // звук объекта в отдельном аудиосурсе
        {
            _objectsAudioSource.clip = _objectsSounds[objectIndex];
            _objectsAudioSource.pitch = Random.Range(0.9f, 1.1f);
            _objectsAudioSource.Play();
        }
    }

    public void PlayCoinSound()
    {
        _coinAudioSource.Play();
    }
    
    public void PlayBrownPlatformSound()
    {
        _brownPlaformAudioSource.pitch = Random.Range(0.9f, 1.1f);
        _brownPlaformAudioSource.Play();
    }

    //public void PlayJumpSound()
    //{
    //    _playerJumpSoundSource.Play();
    //}

    //public void PlayDieSound()
    //{
    //    _playerDieSound.Play();
    //}

    //public void PlayObjectSound(int indexOfObject)
    //{
    //    _objectsSounds[indexOfObject].Play();
    //}
}
