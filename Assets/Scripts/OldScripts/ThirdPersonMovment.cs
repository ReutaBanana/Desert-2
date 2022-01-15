using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovment : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    [SerializeField] Transform cam;
    private Vector3 forward;
    private Vector3 right;
    public Vector3 desiredMoveDirection;
    Vector3 velocityY;
    bool isGrounded;
    bool isClimbing;
    [SerializeField] float gravity=-9.8f;
    [SerializeField] float dragDownForce=1f;
    [SerializeField] private float groundcheckRadius=0.3f;
    [SerializeField] private float yOffset=0.85f;
    [SerializeField] private LayerMask floorMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isClimbing = Physics.Raycast(transform.position, transform.forward, out hitwall, wallCheckDist, climbLayer);
        isGrounded = Physics.CheckSphere(controller.transform.position - new Vector3(0, yOffset, 0), groundcheckRadius, floorMask);

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        forward = cam.transform.forward;
        right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = new Vector3(horiz, 0f, vert);
        controller.Move(direction * speed * Time.deltaTime);

        Gravity();
        //desiredMoveDirection = (forward * vert + right * horiz).normalized * speed;
        //controller.Move(desiredMoveDirection * Time.deltaTime);

    }

    private void Gravity()
    {
        //if (!isClimbing)
        //{
        //if we're touching the ground, add a bit of gravity so we're always on the floor
        if (isGrounded && velocityY.y < 0)
        {
            velocityY = new Vector3(0, -2 * Time.deltaTime, 0);


            }
            else
            {
                //if we're in the air, add some gravity every frame
                velocityY += new Vector3(0, gravity * Time.deltaTime, 0);
            }
        //}
        //else
        //{
        //    //drags us down if we're climbing
        //    velocityY.y = -dragDownForce;
        //}
    }
}
