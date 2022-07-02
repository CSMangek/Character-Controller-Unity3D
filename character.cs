using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;

    private Vector3 playerInput;

    public CharacterController player;
    public float playerSpeed;
    public float gravity;  
    public float fallVelocity;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public float jumpForce = 8f;
    public bool isJumping = false;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerInput();

        CamDirection();

        PlayerMovement();        

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);  

        Debug.Log(player.isGrounded);
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


    public void SetGravity()         
    {
        if (player.isGrounded) 
        {                       
            fallVelocity = -gravity * Time.deltaTime; 
            movePlayer.y = fallVelocity; 
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime; 
            movePlayer.y = fallVelocity; 
        }        
    }
}
