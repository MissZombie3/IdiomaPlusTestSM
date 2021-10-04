using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float fallVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private CharacterController player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator playerAnimatorController;

    private float horizontalmove;
    private float verticalmove;
    private Vector3 playerInput;
    private Vector3 playerMove;
    private Vector3 cameraForward;
    private Vector3 cameraRight;


    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontalmove = Input.GetAxis("Horizontal");
        verticalmove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalmove, 0, verticalmove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        playerAnimatorController.SetFloat("speedWalk", playerInput.magnitude * playerSpeed);

        SetNormalizedCameraDirections();

        playerMove = playerInput.x * cameraRight + playerInput.z * cameraForward;
        playerMove = playerMove * playerSpeed;
        player.transform.LookAt(player.transform.position + playerMove);
        
        SetGravity();
        TrySetJump();

        player.Move(playerMove * Time.deltaTime);
        playerAnimatorController.SetBool("isGrounded", player.isGrounded);
    }

    private void TrySetJump()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            playerMove.y = fallVelocity;
            playerAnimatorController.SetTrigger("jump");
        }
    }

    private void SetNormalizedCameraDirections()
    {
        cameraForward = mainCamera.transform.forward;
        cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
    }

    private void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            playerMove.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            playerMove.y = fallVelocity;
            playerAnimatorController.SetFloat("verticalSpeed", player.velocity.y);
        }
    }
}
