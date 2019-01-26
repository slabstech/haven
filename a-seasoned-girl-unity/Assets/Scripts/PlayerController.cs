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

    public GameObject Bow;
    public GameObject MeeleWeapon;

    public bool meleeActive = false;
    public bool bowActive = false;

    public bool inAir = false;

    public int virtualCameraActive = 1;

    public GameObject arrow;

    public int jumps = 2;

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
        if (virtualCameraActive == 1)
        {
            if (inputMovement > 0.5)
            {
                virtualCameraActive *= -1;
                leftCamera.gameObject.SetActive(virtualCameraActive == -1);
                rightCamera.gameObject.SetActive(virtualCameraActive != -1);
                this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
            }
        }
        else
        {
            if (inputMovement < -0.5)
            {
                virtualCameraActive *= -1;
                leftCamera.gameObject.SetActive(virtualCameraActive == -1);
                rightCamera.gameObject.SetActive(virtualCameraActive != -1);
                this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
            }
        }

        if (Input.GetKeyDown("space"))
        {
            if (jumps > 1)
            {
                if (jumps == 2)
                {
                    animator.SetTrigger("jump");
                }
                jumps--;
                rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Force);
                animator.SetBool("inAir", true);
                inAir = true;
            }
        }

        if (Input.GetButtonDown("Fire2") && meleeActive == false)
        {
            StartMeleeAttack();
        }

        if (Input.GetButtonDown("Fire1") && bowActive == false)
        {
            StartBowAttack();


        }
        if (Input.GetButtonUp("Fire1") && bowActive == true)
        {
            ShootBow();
        }

        if (inAir == true)
        {
            // check for landing
            if (rigid.velocity.y <= 0 && IsGrounded())
            {
                inAir = false;
                animator.SetBool("inAir", false);
                jumps = 2;
            }
        }

        IsGrounded();
    }

    bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(0, -0.05f, 0), Vector2.down);
        Debug.DrawRay(transform.position + new Vector3(0, -0.03f, 0), Vector2.down, Color.red, 10f);
     //   print(hit2D.distance);
        return hit2D.distance < 0.2;
    }

    IEnumerator AddJumpingForce()
    {
        yield return new WaitForSeconds(0.2f);

    }

    IEnumerator SpawnArrow()
    {
        yield return new WaitForSeconds(0.2f);
        if (bowActive == true)
        {
            arrow = Instantiate<GameObject>(Resources.Load("Arrow", typeof(GameObject)) as GameObject, this.transform);
        }

    }

    void StartMeleeAttack()
    {
        animator.SetTrigger("meleeActive");
    }

    void StartBowAttack()
    {
        animator.SetBool("bowActive", true);
        StartCoroutine("SpawnArrow");
        bowActive = true;
    }

    void ShootBow()
    {
        animator.SetBool("bowActive", false);
        bowActive = false;
        if (arrow != null)
        {
            arrow.GetComponent<Rigidbody2D>().isKinematic = false;
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000 * virtualCameraActive, 0), ForceMode2D.Force);
            arrow.transform.SetParent(null);
            arrow = null;
        }

    }

    public void EndMeleeAttack()
    {
        meleeActive = false;

    }
}
