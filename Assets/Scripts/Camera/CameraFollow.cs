using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    public static CameraFollow Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<CameraFollow>();
            }
            return instance;
        }
    }
    [SerializeField] Player player;
    [SerializeField] float cameraSpeed;
    [SerializeField] Camera mainCamera;
    public bool canDie = true;
    Coroutine movement;
    private void Update() {
        if(Vector2.Distance(this.transform.position, player.transform.position) >= 12f && canDie){
            Debug.Log("Dead");
            canDie = false;
            AudioController.Instance.PlaySound("deathSoundTrue");
            CameraFollow.Instance.OnCameraShake(.25f, 1f);
            this.gameObject.SpawnParticle("DeathParticle", player.transform, Vector3.zero, Engine.Instance.Restart);
        }
    }
    public void StartMovement(Vector2 pos){
        if(movement == null)movement = StartCoroutine(Movement(pos));
    }
    IEnumerator Movement(Vector2 pos){
        while(Vector2.Distance(this.transform.position, pos) >= .15f){
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, pos.y), cameraSpeed * Time.deltaTime);
            yield return null;
        }
        movement = null;
    }
    Coroutine shakeRoutine;
    public void OnCameraShake(float duration, float magnitude) {
        if (shakeRoutine != null) {
            return;
        }
        shakeRoutine = StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos3 = mainCamera.transform.localPosition;
        Vector2 originalPos = mainCamera.transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            mainCamera.transform.localPosition = new Vector3(originalPos.x + Random.insideUnitCircle.x * magnitude, originalPos.y + Random.insideUnitCircle.y * magnitude, -10f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos3;
        //DefaultPosition();
        shakeRoutine = null;
    }
}
