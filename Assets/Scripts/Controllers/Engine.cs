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
    public Checkpoint currentCheckpoint;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            player.transform.position = currentCheckpoint.playerSpawnPos.position;
        }
    }
    public void Restart(){
        player.transform.position = currentCheckpoint.playerSpawnPos.position;
    }
}
