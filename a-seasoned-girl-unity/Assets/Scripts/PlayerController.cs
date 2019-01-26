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

    Rigidbody2D rigid;

    Vector3 velocity;

    public CinemachineVirtualCamera leftCamera;
    public CinemachineVirtualCamera rightCamera;

    public Animator animator;

    public bool meleeActive = false;

    public int virtualCameraActive = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        rigid.AddForce(new Vector3(inputMovement, 0, 0) * movementForce, ForceMode2D.Force);
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
        inputMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(inputMovement));
        if (inputMovement == virtualCameraActive)
        {
            virtualCameraActive *= -1;
            if (virtualCameraActive == -1)
            {
                leftCamera.gameObject.SetActive(true);
                rightCamera.gameObject.SetActive(false);
                animator.SetTrigger("turnRight");
            }
            else
            {
                leftCamera.gameObject.SetActive(false);
                rightCamera.gameObject.SetActive(true);
                animator.SetTrigger("turnLeft");

            }
        }

        if (Input.GetKeyDown("space"))
        {
            if (IsGrounded())
            {
                rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Force);
            }
        }

        if (Input.GetButton("Fire1") && meleeActive == false)
        {
            StartMeleeAttack();
        }
        IsGrounded();
    }

    bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down);
        return hit2D.distance < 0.1;
    }

    void StartMeleeAttack()
    {
        animator.SetTrigger("meleeActive");
    }

    public void EndMeleeAttack()
    {
        meleeActive = false;

    }
}
