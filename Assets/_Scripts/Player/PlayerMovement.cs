using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool isJumping;
    
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDir;

    private float airSpeed;
    private float groundSpeed;
    
    
    private void Start()
    {
        rb.freezeRotation = true;
        airSpeed = moveSpeed / 3.5f;
        groundSpeed = moveSpeed;
    }

    private void Update()
    {
        MoveSlowerOnAir();
        GetInput();
        ControlDrag();
        ControlSpeed();
    }

    private void MoveSlowerOnAir()
    {
        
        if (!GroundCheck.isGrounded)
        {
            moveSpeed = airSpeed;
        }
        else
        {
            moveSpeed = groundSpeed;
        }
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void ControlDrag()
    {
        if (GroundCheck.isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && GroundCheck.isGrounded)
        {
            isJumping = true;
            
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Move()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (GroundCheck.isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!GroundCheck.isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void ControlSpeed()
    {
        var movement = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (movement.magnitude > moveSpeed)
        {
            var limitedMovement = movement.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedMovement.x, rb.velocity.y, limitedMovement.z);
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        isJumping = false;
    }
}
