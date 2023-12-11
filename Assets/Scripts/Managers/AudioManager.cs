using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;
    [SerializeField] private AudioClip touchSound;
    [SerializeField] private float audioVolume;

    void Start()
    {
        Init();
    }
    private void Init()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = touchSound;
        audioSource.volume = audioVolume;
    }
    public void PlayTouchSound()
    {
        audioSource.Play();
    }
}
