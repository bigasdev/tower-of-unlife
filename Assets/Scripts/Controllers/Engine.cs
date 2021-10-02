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
    public int currentPlayerHealth = 3;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            Death();
        }
    }
    public void Restart(){
        player.transform.position = currentCheckpoint.playerSpawnPos.position;
        CameraFollow.Instance.canDie = true;
    }
    public void Death(){
        currentPlayerHealth = 3;
        player.transform.position = majorCheckpoint.playerSpawnPos.position;
    }
}
