using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [TextArea]
    [SerializeField] string information;
    [SerializeField] InformationUI informationUI;
    [SerializeField] AudioClip popupSound;
    [SerializeField] Animator tip;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null)return;
        /*if(informationUI == null){
            //AudioController.Instance.PlaySound(popupSound);
            informationUI = UIWrapper.SpawnInfo(information);
        }*/
        tip.SetTrigger("Enter");
    }
    private void OnTriggerExit2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null)return;
        /*if(informationUI != null){
            informationUI.animator.SetTrigger("Leave");
            informationUI = null;
        }*/
        tip.SetTrigger("Leave");
    }
}
