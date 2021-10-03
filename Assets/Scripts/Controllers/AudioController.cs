using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    public static AudioController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<AudioController>();
            }
            return instance;
        }
    }
    public AudioSource music, sfx;
    private void Start() {
        music.volume = PlayerPrefs.GetFloat("MusicVolume") <= 0 ? 1f : PlayerPrefs.GetFloat("MusicVolume");
        sfx.volume = PlayerPrefs.GetFloat("SoundVolume") <= 0 ? 1f : PlayerPrefs.GetFloat("MusicVolume");
    }
    public void PlaySound(AudioClip clip){
        if(sfx.clip == clip || clip == null){
            return;
        }
        sfx.PlayOneShot(clip);
    }
    public void PlaySound(string clip){
        if(string.IsNullOrEmpty(clip)){
            return;
        }
        var c = Resources.Load<AudioClip>("Audio/SFX/" + clip);
        sfx.PlayOneShot(c);
    }
}
