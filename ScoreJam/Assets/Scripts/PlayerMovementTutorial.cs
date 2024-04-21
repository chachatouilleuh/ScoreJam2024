using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float sprintMultiplier = 2f;
    public float groundDrag = 6f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    public float airMultiplier = 0.5f;
    public KeyCode jumpKey = KeyCode.Space;
    public LayerMask whatIsGround;
    public Transform orientation;

    private Rigidbody rb;
    public Animator playerAnimator;
    private bool grounded;
    private bool readyToJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GroundCheck();
        MovementInput();
        HandleJump();
        HandleSprint();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.3f, whatIsGround);
    }

    private void MovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize();

        if (grounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        playerAnimator.SetBool("Run", moveDirection.magnitude > 0);

        SpeedControl();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetBool("Jump", true);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void HandleSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= sprintMultiplier;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed /= sprintMultiplier;
    }

    private void MovePlayer()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
        playerAnimator.SetBool("Jump", false);
    }
}
