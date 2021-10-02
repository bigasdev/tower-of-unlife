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
    public Checkpoint currentCheckpoint;
}
