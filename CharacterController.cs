using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float horizontalMove;
    public float verticalMove;

    private Vector3 PlayerInput;

    public CharacterController player;
    public float PlayerSpeed;
    public float gravity;
    public float fallvelocity;
    
    public Camera mainCamera;
    private Vector3 camForward;
    private vector3 camRights;
    private Vector3 movePlayer;

    public float jumpForce = 8f;
    public bool isJumping = false;

    void Start () 
    {
        player = GetComponent<CharacterController>();
    }

    void Update () 
    {
       PlayerInput();
       CamDirection();
       PlayerMovement();
       SetGravity();
       PlayerSkills();
       player.Move(MovePlayer * Time.deltaTime);
       Debug.Log(Player.isGrounded);
    }

    public void PlayerInput()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
    }

    public void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void PlayerMovement()
    {
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);
    }

    private void FixedUpdate()
    {
        player.Move(new Vector3(horizontalMove, 0, verticalMove) * PlayerSpeed * Time.deltaTime);
    }

    public void SetGravity () {
        if (player.isGrounded)
        {
            fallvelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallvelocity;
        }else
        {
            fallvelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallvelocity;
        }
    }

    public void PlayerSkills()
    {
        if (player.isGrounded)
        {
            isJumping = false;
        }
                
        if (player.isGrounded && Input.GetButtonDown("Jump"))
         {
            isJumping = true;
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
         }
    }
}