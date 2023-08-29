using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip music;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();
        SetMusic();
    }
    public void SetMusic()
    {
        if(PlayerPrefs.GetInt("Music",1) == 1)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
    }
    public void FadeAway()
    {
        StartCoroutine(FadeAwayCoroutine());
    }
    private IEnumerator FadeAwayCoroutine()
    {
        for(int i = 0; i < 100; i++)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
