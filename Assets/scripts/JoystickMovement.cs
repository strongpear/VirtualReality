using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.EventSystems;
public class JoystickMovement : MonoBehaviour
{
    public Rigidbody player;
    public float speed = 5;
    private Vector3 forwardDirection;
    // Start is called before the first frame update
    void Start()
    {
        forwardDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);

        Vector3 moveDirection = transform.right * joystickAxis.x + forwardDirection * joystickAxis.y;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        player.position += moveDirection * speed * Time.deltaTime;
        player.position = new Vector3(player.position.x, 0, player.position.z);


        // Recalculate forward direction
        forwardDirection = Vector3.ProjectOnPlane(transform.forward, transform.position.normalized).normalized;
    }
}
