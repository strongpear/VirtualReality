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

    public float speed = 3f;


    private InputData inputData;
    private Rigidbody myRB;
    private Transform myXrRig;

    
    private Vector3 headsetForward;
    void Start()
    {
        myView = GetComponent<PhotonView>();

        myChild = transform.GetChild(0).gameObject;
        myRB = GetComponent<Rigidbody>();
        
        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;
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
                headsetForward = myXrRig.forward;
            }
        }
    }

    private void FixedUpdate()
    {
        myRB.AddForce(xInput * speed, 0, yInput * speed);
        //transform.Translate(headsetForward * speed * Time.deltaTime * yInput);
    }
}