using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSmoothTime;
    [SerializeField] private float gravityStrength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private CharacterController _controller;
    private Vector3 _currentMoveVelocity;
    private Vector3 _moveDampVelocity;
    private Vector3 _currentForceVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var playerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0,
            z = Input.GetAxisRaw("Vertical")
        };
        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        var moveVector = transform.TransformDirection(playerInput);
        var currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        
        _currentMoveVelocity = Vector3.SmoothDamp(
            _currentMoveVelocity,
            moveVector*currentSpeed,
            ref _moveDampVelocity,
            moveSmoothTime);

        _controller.Move(_currentMoveVelocity * Time.deltaTime);

        var groundCheckRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundCheckRay, 1.25f))
        {
            _currentForceVelocity.y = -2f; // iterar sobre el valor
            if (Input.GetKey(KeyCode.Space))
            {
                _currentForceVelocity.y = jumpStrength;
            }
        }
        else
        {
            _currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        }

        _controller.Move(_currentForceVelocity * Time.deltaTime);
    }
}
