using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpIncrease;
    [SerializeField] float gravityPower;
    [SerializeField] float gravityIncrease;
    [SerializeField] LayerMask groundMask;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] Animator animator;
    [SerializeField] Transform sprite;
    [SerializeField] AudioClip jumpSound;
    public bool jumping, onGround, lookingRight;
    float gravity = 0, jump = 0;
    public float jumpButtonHold, onGroundTimer;
    public Shader teste;
    public Coroutine jumpCoroutine, groundCoroutine;
    public GameObject pauseMenu;
    public static bool canMove = true;
    private void Start() {
        UIWrapper.SpawnLevelName("Tower of fall");
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.SetActive(true);
        }
        if(!canMove)return;
        this.transform.localScale = new Vector3(lookingRight ? 1 : -1, 1, 1);
        animator.SetBool("Falling", !onGround);
        animator.SetBool("Walking", Input.GetAxisRaw("Horizontal") != 0);
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            lookingRight = false;
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            lookingRight = true;
        }
        if(!onGround)return;
        if(Input.GetKey(KeyCode.Space)){
            /*jumpButtonHold += 3.5f * Time.deltaTime;
            if(jumpButtonHold >= 1f){
                StartJump();
                return;
            }*/
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            StartJump();
        }
    }
    private void FixedUpdate() {
        if(!canMove)return;
        this.transform.position = Vector2.MoveTowards(this.transform.position, LookingForWall() ? new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) : this.transform.position + this.transform.right * Input.GetAxisRaw("Horizontal"), walkSpeed * Time.deltaTime);
        HandleGravity();
        HandleInsideGround();
    }
    void HandleGravity(){
        if(jumping)return;
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(this.transform.position, -this.transform.up, .585f, groundMask);
        if(hit2D){
            /*var vectorToTarget = this.transform.position - new Vector3(this.transform.position.x, hit2D.distance, this.transform.position.z);
            vectorToTarget.x = 0;
            Debug.Log(hit2D.distance);
            var distance = vectorToTarget.magnitude;*/
            onGround = true;
            walkSpeed = 5;
            onGroundTimer += 1 * Time.deltaTime;
            gravity = 0;
            ResetCoroutines();
            jumpCoroutine = null;
        }else{
            gravity = Mathf.MoveTowards(gravity, gravityPower, gravityIncrease * Time.deltaTime);
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y - 1), gravity * Time.deltaTime);
            if(onGroundTimer >= .05f){
                if(groundCoroutine == null)groundCoroutine = StartCoroutine(ResetJump());
            }else onGround = false;
        }
    }
    private void OnDrawGizmos() {
        Debug.DrawRay(this.transform.position, -this.transform.up, Color.red, 1f);
        Debug.DrawRay(this.transform.position, Vector2.right * Input.GetAxisRaw("Horizontal"), Color.blue, .25f);
    }
    public void ResetCoroutines(){
        jumpCoroutine = null;
        groundCoroutine = null;
        StopAllCoroutines();
    }
    IEnumerator ResetJump(){
        yield return new WaitForSeconds(.45f);
        onGroundTimer = 0f;
        onGround = false;
        groundCoroutine = null;
    }
    public void StartJump(){
        if(jumpCoroutine != null)return;
        this.gameObject.SpawnParticle(Engine.Instance.particleJumpName, this.transform, new Vector3(0,-.5f,0));
        jumping = true;
        if(Input.GetAxisRaw("Horizontal") != 0){
            walkSpeed = 9;
        }
        AudioController.Instance.PlaySound(jumpSound);
        spriteManager.Squash(.6f, 1f);
        jumpCoroutine = StartCoroutine(Jump(1));
    }
    public bool LookingForWall(){
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(this.transform.position, Vector2.right * Input.GetAxisRaw("Horizontal"), .65f, groundMask);
        if(hit2D){
            return true;
        }
        return false;
    }
    void HandleInsideGround(){
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(this.transform.position, -this.transform.up, .35f, groundMask);
        if(hit2D){
            Debug.Log("Ongroud");
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y + .25f), 50 * Time.deltaTime);
        }
    }
    public bool LookingForWallWithParameters(float parameter){
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(this.transform.position, Vector2.right * parameter, .65f, groundMask);
        if(hit2D){
            return true;
        }
        return false;
    }
    IEnumerator Jump(float increase){
        if(increase <= .05f)increase = .05f;
        if(increase >= .65f)increase = .65f;
        var jumpDistance = this.transform.position + this.transform.up * jumpPower * increase;
        var vectorToTarget = this.transform.position - jumpDistance;
        vectorToTarget.x = 0;
        var distance = vectorToTarget.magnitude;
        jump = 2;
        while(distance >= .15f){
            RaycastHit2D hit2D;
            hit2D = Physics2D.Raycast(this.transform.position, this.transform.up,.5f, groundMask);
            if(hit2D){
                jumpButtonHold = 0f;
                jump = 0;
                jumping = false;
                jumpCoroutine = null;
                yield break;
            }

            vectorToTarget = this.transform.position - jumpDistance;
            vectorToTarget.x = 0;
            distance = vectorToTarget.magnitude;
            jump = Mathf.MoveTowards(jump, jumpSpeed, jumpIncrease * Time.fixedDeltaTime);
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, jumpDistance.y), jumpSpeed * Time.deltaTime);
            yield return null;
        }
        jumpButtonHold = 0f;
        jump = 0;
        jumpPower = 5;
        walkSpeed = 5;
        jumping = false;
    }
}
