using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{    public float moveSpeed = 5f;
    public float sensitivity = 2f;
    public float jumpForce = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Move the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveDirection.Normalize();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity;
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.Rotate(Vector3.right * mouseY);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
