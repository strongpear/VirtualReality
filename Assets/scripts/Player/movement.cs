using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;


public class movement : MonoBehaviour
{ 
    private PhotonView myView;
    private GameObject myChild;

    private float xInput;
    private float yInput;

    public float speed = 5f;


    private InputData inputData;
    private Rigidbody myRB;
    private Transform myXrRig;

    private Transform xrCamera;
    private Vector3 headsetForward;
    private Vector3 forwardDirection;
    void Start()
    {
        myView = GetComponent<PhotonView>();

        myChild = transform.GetChild(0).gameObject;
        myRB = GetComponent<Rigidbody>();
        
        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;
        //xrCamera = myXrRig.GetChild(0).gameObject.GetChild(0).cameraGameObject.transform;
        inputData = myXrOrigin.GetComponent<InputData>();   
    }


    void Update()
    {
        // Get input from the left analog stick

        //Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // // Check if the left analog stick is pushed forward
        // if (thumbstickInput.y > 0)
        // {
        //     // Get the direction that the headset is facing
        //     headsetForward = Camera.main.transform.forward;
        //     headsetForward.y = 0f; // Ensure movement is horizontal only

        //     // Apply movement in the direction the headset is facing
        //     transform.Translate(headsetForward * speed * Time.deltaTime);
        // }

        if (myView.IsMine)
        {
            myXrRig.position = myChild.transform.position;
            
            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;


                // Handle player movement
                float moveHorizontal = xInput;
                float moveVertical = yInput;

                // Calculate movement direction based on player orientation
                Vector3 moveDirection = Camera.main.transform.right * moveHorizontal + forwardDirection * moveVertical;
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

                // Move the player
                transform.position += moveDirection * speed * Time.deltaTime;

                // Recalculate forward direction
                forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Camera.main.transform.position.normalized).normalized;


                // headsetForward = myXrRig.forward;
                // headsetForward = Camera.main.transform.forward;
                // headsetForward.y = 0f; // Ensure movement is horizontal only
            }
        }
    }

    // private void FixedUpdate()
    // {

    //     headsetForward = Vector3.ProjectOnPlane(myXrRig.transform.forward, myXrRig.transform.position.normalized).normalized;
    //     // Calculate movement direction based on player orientation
    //     Vector3 moveDirection = transform.right * xInput + headsetForward * yInput;
    //     moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

    //     // Move the player
    //     transform.position += moveDirection * speed * Time.deltaTime;


    //     // Vector3 movementDirection = xrCamera.forward * yInput + xrCamera.right * xInput;
    //     // movementDirection.y = 0; // Ensure movement is horizontal only
    //     // myRB.AddForce(movementDirection.normalized * speed, ForceMode.VelocityChange);
    //     myRB.AddForce(xInput * speed, 0, yInput * speed);
    //     //transform.Translate(headsetForward * speed * Time.deltaTime * yInput);
    // }
}