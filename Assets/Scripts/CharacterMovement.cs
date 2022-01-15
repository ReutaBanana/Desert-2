using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  
     CharacterController controller;
    [SerializeField] Animator characterAnimator;

    [SerializeField] Transform cam;
    [SerializeField] float movmentSpeed = 3f;
    [SerializeField] float runMovmentSpeed = 2f;
    [SerializeField] float groundedGravity = -0.5f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float initialJumpVelocity;

    private MovingObstacleLogic script;
    private Vector3 desiredDirection;
    private Vector3 forward;
    private Vector3 right;
    private Vector3 velocityY;
    float horizontal;
    float vertical;
    float maxJumpHeight = 0.3f;
    float maxJumpTime = 0.25f;

    public bool holdingCrate;
    bool isJumping = false;
    bool isRuning = false;

    int jumpCount = 0;
    Dictionary<int, float> inititalJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();

    [SerializeField] GameObject keyOrangeUI;
    [SerializeField] GameObject keyBlueUI;
    public bool hasOrangeKey = false;
    public bool hasBlueKey = false;

    public AudioSource jumpingSound;
    private bool isDead;

    [SerializeField] GameObject lostScreen;
    private GameObject gameMananger;

    private void Start()
    {
        gameMananger = GameObject.Find("GameMananger");
    }
    private void Awake()
    {
        SetUpJumpVariables();
        controller=gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Movment();
        HandleAnimation();
        HandleRotation();
        Gravity();
        Jumping();
    }

    private void HandleAnimation()
    {
        characterAnimator.SetBool("isRuning", isRuning);
        characterAnimator.SetBool("isJumping", isJumping);
        characterAnimator.SetBool("holdingBoxIdle", holdingCrate);
        if (horizontal == 0 && vertical == 0)
        {
            characterAnimator.SetBool("isWalking", false);
        }
        else
        {
            characterAnimator.SetBool("isWalking", true);
        }
    }

    private void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight / Mathf.Pow(timeToApex, 2));
        initialJumpVelocity = ((2 * maxJumpHeight) / timeToApex);
    }
    private void Jumping()
    {
        if (controller.isGrounded && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {    
                velocityY.y = initialJumpVelocity;
                isJumping = true;
                jumpingSound.Play();
            }
        }
        else if(controller.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
    private void Movment()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        forward = cam.transform.forward;
        right = cam.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        controller.Move(desiredDirection * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            desiredDirection = (forward * vertical + right * horizontal).normalized * movmentSpeed * runMovmentSpeed;
            desiredDirection += velocityY;
            controller.Move(desiredDirection * Time.deltaTime);
            isRuning = true;
        }
        else
        {
            desiredDirection = (forward * vertical + right * horizontal).normalized * movmentSpeed;
            desiredDirection += velocityY;
            controller.Move(desiredDirection * Time.deltaTime);
            isRuning = false;
        }
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        if (horizontal != 0 || vertical != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(positionToLookAt), 9 * Time.deltaTime);
        }
    }

    private void Gravity()
    {
        bool isFalling = velocityY.y <= 0.0f||!Input.GetKey(KeyCode.Space);
        if (controller.isGrounded)
        {
            velocityY = new Vector3(0f, groundedGravity, 0f);
        }
        else if (isFalling)
        {
            float previousYVelocity = velocityY.y;
            float newYVelocity = velocityY.y + (gravity* Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f,-20f);
            velocityY.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = velocityY.y;
            float newYVelocity = velocityY.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            velocityY.y = nextYVelocity;

        }
    }
    public void GetBlueKey()
    {
        keyBlueUI.SetActive(true);
        hasBlueKey = true;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Key"))
        {
            if (hit.gameObject.GetComponent<KeyCodeColor>().colorKey == "Orange")
            {
                keyOrangeUI.SetActive(true);
                Destroy(hit.gameObject);
                hasOrangeKey = true;
            }

        }
        if (hit.gameObject.CompareTag("Button"))
        {
            script = hit.gameObject.GetComponent<MovingObstacleLogic>();
            script.isCharcterHit = true;
        }
        else
        {
            if (script != null)
            {
                script.isCharcterHit = false;
            }
        }
        if (hit.gameObject.CompareTag("Trap"))
        {
            isDead = true;
            characterAnimator.SetTrigger("isDead");
            StartCoroutine(Lost());
        }
}
    public void destroyKey()
    {
        keyOrangeUI.SetActive(false);
        keyBlueUI.SetActive(false);

    }
    IEnumerator Lost()
    {
        yield return new WaitForSeconds(4);
        lostScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }
}


