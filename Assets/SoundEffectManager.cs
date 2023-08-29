using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetSoundEffects();
    }
    public void SetSoundEffects()
    {
        if(PlayerPrefs.GetInt("SoundEffects",1) == 1)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
    }
    public void PlaySoundEffect(int index)
    {
        audioSource.PlayOneShot(soundEffects[index]);
    }
}