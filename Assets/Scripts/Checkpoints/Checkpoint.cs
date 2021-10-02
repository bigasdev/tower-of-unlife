using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform playerSpawnPos;
    public Transform cameraPos;
    public string levelName;
    [SerializeField] bool changeCamera, showLevelName, triggered, majorCheckpoint = false;
    [SerializeField] Animator checkpoint;
    private void OnTriggerEnter2D(Collider2D other) {
        if(triggered)return;
        var player = other.GetComponent<Player>();
        if(player == null)return;
        checkpoint.SetTrigger("Pop");
        triggered = true;
        Engine.Instance.currentCheckpoint = this;
        if(changeCamera)FindObjectOfType<CameraFollow>().StartMovement(cameraPos.position);
        if(majorCheckpoint)Engine.Instance.majorCheckpoint = this;
        if(showLevelName)UIWrapper.SpawnLevelName(levelName);
    }
}
