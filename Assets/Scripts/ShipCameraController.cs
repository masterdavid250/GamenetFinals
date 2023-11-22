using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCameraController : MonoBehaviour
{
    public Transform player;
    public LayerMask groundLayer;
    public float rotationSpeed = 5f;
    public float maxVerticalAngle = 60f;

    private Vector3 offset;
    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Calculate the initial offset from the player to the camera
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        currentYaw += mouseX;
        currentPitch = Mathf.Clamp(currentPitch - mouseY, -maxVerticalAngle, maxVerticalAngle);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 desiredPosition = player.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(player.position);
    }
}
