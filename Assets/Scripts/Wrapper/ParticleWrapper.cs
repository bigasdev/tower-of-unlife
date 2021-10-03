using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleWrapper
{
    public static ParticleCallback SpawnParticle(this GameObject obj, string particleName, Transform pos, Vector3 offset, System.Action action = null){
        if(!Engine.Instance.particlesEnabled){
            if(action != null)action();
            return null;
        }
        var p = Resources.Load<ParticleCallback>("Prefabs/Particle/" + particleName);
        var particle = Object.Instantiate(p);
        particle.transform.position = pos.position + offset;
        particle.Initialize(action);
        return particle;
    }
}
