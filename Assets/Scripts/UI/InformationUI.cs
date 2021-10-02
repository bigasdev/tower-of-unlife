using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InformationUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mapName;
    public Animator animator;
    public void Initialize(string name){
        mapName.text = name;
    }
}
