using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
public class playermovement : MonoBehaviour
{   

    private PhotonView myView;
    private InputData inputData;
    private float boostTimer;
    private bool boosting;
    public float speed;
    public float mouseSensitivity = 2f;
    private Vector3 forwardDirection;
    private Transform myXrRig;
    
    private float teleportDistance = 5f;
    private int countTeleportPowerUp;
    public LayerMask teleportMask;

    void Start()
    {
        myView = GetComponent<PhotonView>();

        countTeleportPowerUp = 0;
        forwardDirection = transform.forward;
        speed = 7;
        boostTimer = 0;
        boosting = false;

        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;
        inputData = myXrOrigin.GetComponent<InputData>();
    }

    void Update()
    {
        if (myView.IsMine)
        {
            HandleInput();

            // Boost
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
            //if (Input.GetKeyDown(KeyCode.Space) || (inputData.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState))

            // Teleportation
            if (countTeleportPowerUp >= 1 && (Input.GetKeyDown(KeyCode.Space)))
            {
                TeleportForward();
                Debug.Log("Teleporting Player Forward");
                countTeleportPowerUp--;
            }
        }

    }

    // Function to handle teleporting forward
    void TeleportForward()
    {
        // Calculate the teleport destination
        Vector3 teleportDestination = transform.position + transform.forward * teleportDistance;

        // Perform a raycast to check if the teleport destination is valid
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, teleportDistance, teleportMask))
        {
            // If the raycast hits something within the teleport distance, adjust the destination
            teleportDestination = hit.point - transform.forward * 0.5f; // Adjust slightly back from the hit point
        }

        // Teleport the player to the destination
        transform.position = teleportDestination;
    }

    // Function to collect the teleport power-up
    public void CollectTeleportPowerUp()
    {   
        Debug.Log("Teleport Collected");
        countTeleportPowerUp++;
        // You can add any visual/audio effects or UI feedback here to indicate that the power-up has been collected
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
        forwardDirection = Vector3.ProjectOnPlane(myXrRig.transform.forward, myXrRig.transform.position.normalized).normalized;
    }




}
