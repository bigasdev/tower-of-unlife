using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform playerSpawnPos;
    [SerializeField] bool changeCamera;
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null)return;
        Engine.Instance.currentCheckpoint = this;
    }
}
