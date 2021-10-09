using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private static Engine instance;
    public static Engine Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<Engine>();
            }
            return instance;
        }
    }
    public Player player;
    public Checkpoint currentCheckpoint, majorCheckpoint;
    public Health health;
    public int currentPlayerHealth = 3;
    public bool particlesEnabled, deathEnabled;
    public string particleJumpName;
    private void Start() {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("Particles"))){
            PlayerPrefs.SetString("Particles", "Enabled");
        }
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("Death"))){
            PlayerPrefs.SetString("Death", "Enabled");
        }
        particlesEnabled = PlayerPrefs.GetString("Particles") == "Enabled" ? true : false;
        deathEnabled = PlayerPrefs.GetString("Death") == "Enabled" ? true : false;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            Reset();
        }
    }
    public void Reset(){
        AudioController.Instance.PlaySound("deathSoundTrue");
        CameraFollow.Instance.OnCameraShake(.25f, 1f);
        this.gameObject.SpawnParticle("DeathParticle",  player.transform, Vector3.zero, Death);
        player.gameObject.SetActive(false);
    }
    public void Restart(){
        player.jumping = false;
        player.transform.position = currentCheckpoint.playerSpawnPos.position;
        FindObjectOfType<WindManager>().ChangeWindToDefault();
        CameraFollow.Instance.canDie = true;
        DamagePlayer();
    }
    public void Death(){
        currentPlayerHealth = 3;
        player.jumping = false;
        player.transform.position = majorCheckpoint.playerSpawnPos.position;
        player.gameObject.SetActive(true);
        CameraFollow.Instance.transform.position = majorCheckpoint.cameraPos.transform.position;
        var checkpoints = FindObjectsOfType<Checkpoint>();
        foreach(var c in checkpoints){
            c.triggered = false;
        }
        health.Reset();
    }
    public void DamagePlayer(){
        if(!deathEnabled)return;
        if(currentPlayerHealth > 0)health.DoDamage(currentPlayerHealth - 1);
        currentPlayerHealth--;
        if(currentPlayerHealth <= 0){
            Death();
        }
    }
}
