using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DamagePlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null)return;
        AudioController.Instance.PlaySound("deathSoundTrue");
        CameraFollow.Instance.OnCameraShake(.25f, 1f);
        this.gameObject.SpawnParticle("DeathParticle",  player.transform, Vector3.zero);
        Engine.Instance.Restart();
    }
}
