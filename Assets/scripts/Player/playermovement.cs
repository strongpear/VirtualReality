using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{ 
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private Vector3 forwardDirection;

    void Start()
    {
        forwardDirection = transform.forward;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Handle player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on player orientation
        Vector3 moveDirection = transform.right * moveHorizontal + forwardDirection * moveVertical;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // Move the player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.Rotate(-Vector3.right * mouseY);

        // Recalculate forward direction
        forwardDirection = Vector3.ProjectOnPlane(transform.forward, transform.position.normalized).normalized;
    }

}
