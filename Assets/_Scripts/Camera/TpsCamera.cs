using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCamera : MonoBehaviour
{
    [Header("REFERENCES")] 
    [SerializeField] private Transform player; 
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;

    [Header("VALUES")]
    [SerializeField] private float rotationSpeed;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (inputDir != Vector3.zero)
        {
            model.forward = Vector3.Slerp(model.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
