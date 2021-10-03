using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalmove;
    private float verticalmove;
    private Vector3 playerInput;
    private Vector3 playerMove;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public CharacterController player;
    public Camera mainCamera;

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

        SetNormalizedCameraDirections();

        playerMove = playerInput.x * cameraRight + playerInput.z * cameraForward;

        playerMove = playerMove * playerSpeed;

        player.transform.LookAt(player.transform.position + playerMove);
        
        SetGravity();
        SetMovementPlayerSiklls();

        player.Move(playerMove * Time.deltaTime);
    }

    private void SetMovementPlayerSiklls()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            playerMove.y = fallVelocity;
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
        }
        
    }

}
