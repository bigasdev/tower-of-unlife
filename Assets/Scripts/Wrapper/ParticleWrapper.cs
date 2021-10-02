using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleWrapper
{
    public static ParticleCallback SpawnParticle(this GameObject obj, string particleName, Transform pos, System.Action action){
        var p = Resources.Load<ParticleCallback>("Prefabs/Particle/" + particleName);
        var particle = Object.Instantiate(p);
        particle.transform.position = pos.position;
        particle.Initialize(action);
        return particle;
    }
}
