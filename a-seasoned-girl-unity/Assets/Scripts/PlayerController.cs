using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float movementForce = 60;
    public float jumpForce = 200;
    public float maxSpeed = 200;

    public float inputMovement;

    Rigidbody rigid;

    Vector3 velocity;

    public CinemachineVirtualCamera leftCamera;
    public CinemachineVirtualCamera rightCamera;

    public int virtualCameraActive = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigid.AddForce(new Vector3(inputMovement, 0, 0) * movementForce, ForceMode.Acceleration);
        if (rigid.velocity.magnitude > maxSpeed)
        {
            velocity = rigid.velocity.normalized * maxSpeed;
            rigid.velocity = velocity;
        }
        //    Vector3 lookDirecton = new Vector3(look.x, 0, look.y);
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement = -Input.GetAxis("Horizontal");
        if(inputMovement == virtualCameraActive) {
            virtualCameraActive *= -1;
            if(virtualCameraActive == 1) {
                leftCamera.gameObject.SetActive(true);
                rightCamera.gameObject.SetActive(false);
            } else {
                leftCamera.gameObject.SetActive(false);
                rightCamera.gameObject.SetActive(true);

            }
        }
    }
}
