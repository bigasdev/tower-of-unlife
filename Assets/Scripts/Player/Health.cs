using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [SerializeField] Animator[] healths;
    public void DoDamage(int health){
        healths[health].SetTrigger("Damage");
    }
    public void Reset(){
        foreach(var h in healths){
            h.SetTrigger("Reset");
        }
    }
}
