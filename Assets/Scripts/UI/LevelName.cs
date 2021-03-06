using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelName : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI mapName;
    [SerializeField] AudioClip popupSound;
    public void Initialize(string name){
        AudioController.Instance.PlaySound(popupSound);
        mapName.text = name;
    }
}
