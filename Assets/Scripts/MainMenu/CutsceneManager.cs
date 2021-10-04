using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Image cutsceneImage;
    [SerializeField] TextMeshProUGUI dialogueBox;
    [SerializeField] Animator cutsceneAnim, textAnim;
    public float timer, currentTime;
    public int cutsceneStep = 0;
    public List<CutsceneStep> steps;
    private void Start() {
        currentTime = steps[0].time;
        cutsceneImage.sprite = steps[0].image;
        dialogueBox.text = steps[0].dialogue;
    }
    private void Update() {
        timer += Time.deltaTime;
        if(timer >= currentTime){
            TrySkip();
        }
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)){
            TrySkip();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(2);
        }
    }

    private void TrySkip()
    {
        cutsceneAnim.SetTrigger("Leave");
        textAnim.SetTrigger("Leave");
        timer = 0;
        StartCoroutine(WaitFor());
    }
    IEnumerator WaitFor(){
        yield return new WaitForSeconds(1f);
        cutsceneStep++;
        if(cutsceneStep >= steps.Count){
            SceneManager.LoadScene(2);
        }
        currentTime = steps[cutsceneStep].time;
        cutsceneImage.sprite = steps[cutsceneStep].image;
        dialogueBox.text = steps[cutsceneStep].dialogue;
    }
}
[System.Serializable]
public class CutsceneStep{
    public Sprite image;
    [TextArea]
    public string dialogue;
    public float time;
    public CutsceneStep(Sprite image, string dialogue)
    {
        this.image = image;
        this.dialogue = dialogue;
    }
}
