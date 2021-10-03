using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    System.Action action;
    private void Start() {
        var particleSystem = GetComponent<ParticleSystem>();
 
        var main = particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public void Initialize(System.Action act){
        action = act;
    }
    public void OnParticleSystemStopped() {
        Debug.Log("Stop");
        if(action != null){
            action();
        }else{
            this.gameObject.SetActive(false);
        }
    }
}
