using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
public class playermovement : MonoBehaviour
{   

    private PhotonView myView;

    private float boostTimer;
    private bool boosting;
    public float speed;
    public float mouseSensitivity = 2f;
    private Vector3 forwardDirection;
    private Transform myXrRig;
    void Start()
    {
        myView = GetComponent<PhotonView>();

        forwardDirection = transform.forward;
        speed = 7;
        boostTimer = 0;
        boosting = false;

        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;
    }

    void Update()
    {
        if (myView.IsMine)
        {
            HandleInput();

            if(boosting)
            {
                boostTimer += Time.deltaTime;
                if(boostTimer >= 3)
                {
                    speed = 7;
                    boostTimer = 0;
                    boosting = false;
                }
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpeedBoost")
        {
            boosting = true;
            speed = 50;

        }
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
        transform.position += moveDirection * speed * Time.deltaTime;

        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        myXrRig.Rotate(Vector3.up * mouseX);
        //Camera.main.transform.Rotate(-Vector3.right * mouseY);
        myXrRig.Rotate(-Vector3.right * mouseY);

        // Recalculate forward direction
        forwardDirection = Vector3.ProjectOnPlane(transform.forward, transform.position.normalized).normalized;
    }




}
