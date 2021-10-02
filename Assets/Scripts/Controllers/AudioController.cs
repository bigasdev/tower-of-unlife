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
    public void PlaySound(AudioClip clip){
        if(sfx.clip == clip){
            return;
        }
        sfx.PlayOneShot(clip);
    }
}
