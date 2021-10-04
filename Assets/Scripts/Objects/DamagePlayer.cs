using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DamagePlayer : MonoBehaviour
{
    Player player;
    private void OnTriggerEnter2D(Collider2D other) {
        player = other.GetComponent<Player>();
        if(player == null)return;
        AudioController.Instance.PlaySound("deathSoundTrue");
        CameraFollow.Instance.OnCameraShake(.25f, 1f);
        this.gameObject.SpawnParticle("DeathParticle",  player.transform, Vector3.zero, Restart);
        player.gameObject.SetActive(false);
    }
    public void Restart(){
        if(player != null)player.gameObject.SetActive(true);
        Engine.Instance.Restart();
    }
}
