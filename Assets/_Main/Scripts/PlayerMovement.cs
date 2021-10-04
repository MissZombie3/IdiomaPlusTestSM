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
    [SerializeField] private Animator playerAnimatorController;
    [SerializeField] private float damping = 1;

    private Camera mainCamera;
    private CharacterController player;
    private float horizontalmove;
    private float verticalmove;
    private Vector3 playerInput;
    private Vector3 playerMove;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private Health health;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        horizontalmove = health.IsDead? 0 : Input.GetAxis("Horizontal");
        verticalmove = health.IsDead ? 0 : Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalmove, 0, verticalmove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        playerAnimatorController.SetFloat("speedWalk", playerInput.magnitude * playerSpeed);

        SetNormalizedCameraDirections();

        playerMove = playerInput.x * cameraRight + playerInput.z * cameraForward;
        playerMove = playerMove * playerSpeed;

        if (playerInput.magnitude != 0)
        {
            Vector3 targetPos = (transform.position + playerMove);
            Vector3 lookDirection = targetPos - transform.position;
            lookDirection.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * damping);
        }

        SetGravity();
        //TrySetJump();

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
