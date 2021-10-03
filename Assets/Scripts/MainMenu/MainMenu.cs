using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public List<Animator> animators;
    public int currentSelection = 0;
    private void Start() {
        Selection(0);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            Selection(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            Selection(1);
        }
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)){
            Interact();
        }
    }

    private void Interact()
    {
        if(currentSelection == 0){
            SceneManager.LoadScene(1);
        }
        if(currentSelection == 2){
            Application.Quit();
        }
    }

    public void Selection(int value){
        AudioController.Instance.PlaySound("popupsound");
        animators[currentSelection].SetTrigger("Unselect");
        var val = currentSelection + value;
        if(val > 2){
            val = 0;
        }
        if(val < 0){
            val = 2;
        }
        currentSelection = val;
        animators[currentSelection].SetTrigger("Select");
    }
}
