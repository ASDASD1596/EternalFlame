using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class Sounds 
{
    [SerializeField] private SoundManager.SoundName _name;
    public SoundManager.SoundName name => _name;

    [SerializeField] private AudioClip _clip;
    public AudioClip clip => _clip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    public float volume => _volume;

    [SerializeField] private bool _loop;
    public bool loop => _loop;


    [HideInInspector] public AudioSource audioSource;
}
