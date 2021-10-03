using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PauseMenu : MonoBehaviour
{
    public List<Animator> animators;
    public int currentSelection = 0;
    public float volumeChanger = .05f;
    [SerializeField] TextMeshProUGUI soundVolume, musicVolume, particlesEnabled, deathEnabled;
    [SerializeField] Color goodColor, badColor;
    private void OnEnable() {
        currentSelection = 0;
        Selection(0);
        ChangeStuff(0);
        Time.timeScale = .2f;
        Player.canMove = false;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            Selection(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            Selection(1);
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            ChangeStuff(-1);
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            ChangeStuff(1);
        }
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)){
            Interact();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 1f;
            Player.canMove = true;
            this.gameObject.SetActive(false);
        }
    }

    private void Interact()
    {
        if(currentSelection == 4){
            Time.timeScale = 1f;
            Player.canMove = true;
            this.gameObject.SetActive(false);
        }
        if(currentSelection == 5){
            Time.timeScale = 1f;
            Player.canMove = true;
            SceneManager.LoadScene(0);
        }
    }

    public void Selection(int value){
        AudioController.Instance.PlaySound("popupsound");
        animators[currentSelection].SetTrigger("Unselect");
        var val = currentSelection + value;
        if(val > 5){
            val = 0;
        }
        if(val < 0){
            val = 5;
        }
        currentSelection = val;
        animators[currentSelection].SetTrigger("Select");
    }
    public void ChangeStuff(int value){
        if(currentSelection == 0){
            AudioController.Instance.PlaySound("popupsound");
            AudioController.Instance.sfx.volume += .05f * value;
        }
        soundVolume.text = System.Math.Round(AudioController.Instance.sfx.volume * 100, System.MidpointRounding.AwayFromZero).ToString();
        soundVolume.color = Color.Lerp(badColor, goodColor, AudioController.Instance.sfx.volume);
        PlayerPrefs.SetFloat("SoundVolume", AudioController.Instance.sfx.volume);
        if(currentSelection == 1){
            AudioController.Instance.PlaySound("popupsound");
            AudioController.Instance.music.volume += .05f * value;
        }
        musicVolume.text = System.Math.Round(AudioController.Instance.music.volume * 100, System.MidpointRounding.AwayFromZero).ToString();
        musicVolume.color = Color.Lerp(badColor, goodColor, AudioController.Instance.music.volume);
        PlayerPrefs.SetFloat("MusicVolume", AudioController.Instance.music.volume);
        if(currentSelection == 2){
            AudioController.Instance.PlaySound("popupsound");
            Engine.Instance.particlesEnabled = !Engine.Instance.particlesEnabled;
        }
        particlesEnabled.text = Engine.Instance.particlesEnabled ? "YES" : "NO";
        particlesEnabled.color = Engine.Instance.particlesEnabled ? goodColor : badColor;
        PlayerPrefs.SetString("Particles", Engine.Instance.particlesEnabled ? "Enabled" : "NO");
        if(currentSelection == 3){
            AudioController.Instance.PlaySound("popupsound");
            Engine.Instance.deathEnabled = !Engine.Instance.deathEnabled;
        }
        deathEnabled.text = Engine.Instance.deathEnabled ? "YES" : "NO";
        deathEnabled.color = Engine.Instance.deathEnabled ? goodColor : badColor;
        PlayerPrefs.SetString("Death", Engine.Instance.deathEnabled ? "Enabled" : "NO");
        PlayerPrefs.Save();
    }
}
