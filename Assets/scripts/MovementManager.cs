using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class MovementManager : MonoBehaviour
{
    private PhotonView myView;
    private GameObject myChild;

    private float xInput;
    private float yInput;
    private float movementSpeed = 10.0f;

    private InputData inputData;
    //[SerializeField] private GameObject myObjectToMove;
    private Rigidbody myRB;
    private Transform myXrRig;

    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponent<PhotonView>();

        myChild = transform.GetChild(0).gameObject;
        myRB = myChild.GetComponent<Rigidbody>();
        
        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;
        inputData = myXrOrigin.GetComponent<InputData>();   
    }

    // Update is called once per frame
    void Update()
    {
        
        if (myView.IsMine)
        {
            myXrRig.position = myChild.transform.position;
            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;
            }
            else
            {
                xInput = Input.GetAxis("Horizontal");
                yInput = Input.GetAxis("Vertical");
            }    
        }

    }

    private void FixedUpdate()
    {
        myRB.AddForce(xInput * movementSpeed, 0, yInput * movementSpeed);
    }
}
