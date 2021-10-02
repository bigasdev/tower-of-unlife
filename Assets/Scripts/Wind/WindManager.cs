using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Player player;
    [SerializeField] float cameraWindSpeed, playerWindSpeed;
    [SerializeField] float windTimer;
    [SerializeField] AudioClip towerSound;
    public List<WindSettings> settings;
    public int currentWind = 1;
    float wind;
    private void FixedUpdate() {
        wind += 1 * Time.deltaTime;
        if(wind >= windTimer){
            ChangeWind();
            wind -= windTimer;
        }
        if(!player.LookingForWallWithParameters(settings[currentWind].playerWindForce))player.transform.position = Vector2.MoveTowards(player.transform.position, player.transform.position + Vector3.right * settings[currentWind].playerWindForce, playerWindSpeed * Time.deltaTime);
        float angle = Mathf.MoveTowardsAngle(playerCamera.transform.localEulerAngles.z, settings[currentWind].cameraWindForce, cameraWindSpeed * Time.deltaTime);
        playerCamera.transform.eulerAngles = new Vector3(0,0,angle);
    }
    void ChangeWind(){
        CameraFollow.Instance.OnCameraShake(.25f, .45f);
        AudioController.Instance.PlaySound(towerSound);
        currentWind++;
        if(currentWind >= settings.Count){
            currentWind = 0;
        }
    }
}
[System.Serializable]
public class WindSettings{
    public float cameraWindForce, playerWindForce;

    public WindSettings(float cameraWindForce, float playerWindForce)
    {
        this.cameraWindForce = cameraWindForce;
        this.playerWindForce = playerWindForce;
    }
}
