using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Vector2 sensitivities;

    private Vector2 _xyRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var mouseInput = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        _xyRotation.x -= mouseInput.y * sensitivities.y;
        _xyRotation.y += mouseInput.x * sensitivities.x;

        _xyRotation.x = Mathf.Clamp(_xyRotation.x, -90f, 90f); // |-para abajo | +para arriba|
        transform.eulerAngles = new Vector3(0f, _xyRotation.y, 0f);
        playerCamera.localEulerAngles = new Vector3(_xyRotation.x, 0f, 0f);
    }
}
