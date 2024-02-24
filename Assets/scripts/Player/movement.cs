using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{ public float speed = 3f;

    void Update()
    {
        // Get input from the left analog stick
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // Check if the left analog stick is pushed forward
        if (thumbstickInput.y > 0)
        {
            // Get the direction that the headset is facing
            Vector3 headsetForward = Camera.main.transform.forward;
            headsetForward.y = 0f; // Ensure movement is horizontal only

            // Apply movement in the direction the headset is facing
            transform.Translate(headsetForward * speed * Time.deltaTime);
        }
    }
}