using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [SerializeField] float moveSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float gravity;

    [SerializeField] float turnSmoothing;
    Vector3 velocityY;
    [SerializeField]
    public Vector3 desiredMoveDirection;

    bool isGrounded;

    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float groundcheckRadius;
    [SerializeField]
    private LayerMask floorMask;

    bool CanDoubleJump;
    [SerializeField]
    private float secondJumpMultiplier;

    RaycastHit hit;
    public LayerMask layer;

    bool isDashing;
    Vector3 direction;
    private Vector3 forward;

    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    float dashTimer;
    Vector3 dashDir;

    float dashCooldownTimer;
    private Vector3 right;
    private float horiz;
    private float vert;

    bool isClimbing;
    [SerializeField] LayerMask climbLayer;
    [SerializeField] float wallCheckDist;
    [SerializeField] RaycastHit hitwall;

    [SerializeField] float dragDownForce;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check sphere at characters legs to see if we're on the floorhori
        isGrounded = Physics.CheckSphere(controller.transform.position - new Vector3(0, yOffset, 0), groundcheckRadius, floorMask);

        Dash();

        //raycast a short distance forward to see if we can climb
        isClimbing = Physics.Raycast(transform.position, transform.forward, out hitwall, wallCheckDist, climbLayer);

        Gravity();
        Movement();

    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dash");
            if (dashCooldownTimer <= 0)
            {
                //start dash
                isDashing = true;
                dashTimer = dashTime;
                dashDir = (forward * vert + right * horiz).normalized;
            }
        }

        if (isDashing)
        {
            //if there was an input when the dash started
            if (dashDir.magnitude != 0)
            {
                //move towrds the direction pressed
                controller.Move(dashDir * dashSpeed * Time.deltaTime);
            }//if there wasnt
            else
            {
                //move forwards
                controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            }

            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                dashDir = Vector3.zero;
                dashCooldownTimer = dashCooldown;
            }
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
    private void Movement()
    {
        //inptus
        horiz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        
        //camera front and right  vectors
        forward = cam.transform.forward;
        right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        if (!isClimbing)
        {
            //build the direction vector
            desiredMoveDirection = (forward * vert + right * horiz).normalized * moveSpeed;

            //if there is some input we turn towards the vector
            if (horiz != 0 || vert != 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), turnSmoothing * Time.deltaTime);
        }
        else
        {
            //if we're climbing, move up instead of forward
            desiredMoveDirection = (transform.up * vert + right * horiz).normalized * moveSpeed;
        }

        Jump();

        //add y veclocity to our movement
        desiredMoveDirection += velocityY;
        
        //check to lock movement while dashing
        if (!isDashing)
            controller.Move(desiredMoveDirection * Time.deltaTime);


    }


    private void Jump()
    {
        if (!isClimbing)
        {
            if (isGrounded)
            {
                //first jummp
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Jump");

                    velocityY = new Vector3(0, JumpForce, 0);
                    CanDoubleJump = true;
                }
            }
            //second jump
            if (Input.GetKeyDown(KeyCode.Space) && CanDoubleJump && !isGrounded)
            {
                CanDoubleJump = false;
                velocityY = new Vector3(0, JumpForce * secondJumpMultiplier, 0);
            }
        }
        else
        {
            //if we're climbing
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Debug.Log("Jump");

                //jump back 
                velocityY = transform.up * JumpForce + (-transform.forward * JumpForce);
                transform.rotation = Quaternion.LookRotation(-transform.forward);
                CanDoubleJump = true;
            }
        }
    }


    private void Gravity()
    {
        if (!isClimbing)
        {
            //if we're touching the ground, add a bit of gravity so we're always on the floor
            if (isGrounded && velocityY.y < 0)
            {
                velocityY = new Vector3(0, -2 * Time.deltaTime, 0);
                CanDoubleJump = false;


            }
            else
            {
                //if we're in the air, add some gravity every frame
                velocityY += new Vector3(0, gravity * Time.deltaTime, 0);
            }
        }
        else
        {
            //drags us down if we're climbing
            velocityY.y = -dragDownForce;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, yOffset, 0), groundcheckRadius);
    }
}
