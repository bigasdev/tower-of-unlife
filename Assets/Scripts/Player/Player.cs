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
    public bool jumping, onGround, lookingRight;
    float gravity = 0, jump = 0;
    public float jumpButtonHold, onGroundTimer;
    public Coroutine jumpCoroutine, groundCoroutine;
    private void Update() {
        this.transform.localScale = new Vector3(lookingRight ? 1 : -1, 1, 1);
        animator.SetBool("Falling", !onGround);
        animator.SetBool("Walking", Input.GetAxisRaw("Horizontal") != 0);
        if(Input.GetKeyDown(KeyCode.A)){
            lookingRight = false;
        }
        if(Input.GetKeyDown(KeyCode.D)){
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
        this.transform.position = Vector2.MoveTowards(this.transform.position, LookingForWall() ? new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) : this.transform.position + this.transform.right * Input.GetAxisRaw("Horizontal"), walkSpeed * Time.deltaTime);
        HandleGravity();
    }
    void HandleGravity(){
        if(jumping)return;
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(this.transform.position, -this.transform.up, .625f, groundMask);
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
            if(onGroundTimer >= .5f){
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
        yield return new WaitForSeconds(.25f);
        onGroundTimer = 0f;
        onGround = false;
        groundCoroutine = null;
    }
    public void StartJump(){
        jumping = true;
        if(Input.GetAxisRaw("Horizontal") != 0){
            walkSpeed = 9;
        }
        spriteManager.Squash(.6f, 1f);
        if(jumpCoroutine != null)return;
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
        jumpPower = 4;
        walkSpeed = 5;
        jumping = false;
        jumpCoroutine = null;
    }
}
