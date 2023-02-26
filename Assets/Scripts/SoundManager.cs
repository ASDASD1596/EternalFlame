using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _insstance;
    public static SoundManager instance => _insstance;
    [SerializeField] private Sounds[] sounds;
    public enum SoundName
    {
        BGM1,
        Jump,
        Dead,
        Cavecollapse,
        omori,
        Fallingplatform     
    }

    private void Awake()
    {
        if (_insstance == null)
        {
            _insstance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play(SoundName name)
    {
        Sounds sound = GetSound(name);
        if (sound.audioSource == null)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
        }
        sound.audioSource.Play();
    }

    private Sounds GetSound(SoundName name)
    {
        return System.Array.Find(sounds, s => s.name == name);
    }

    public void Mute(SoundName name)
    {
        Sounds sound = GetSound(name);
        sound.audioSource.volume = 0;
    }
}

