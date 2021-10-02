using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelName : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI mapName;
    public void Initialize(string name){
        mapName.text = name;
    }
}
