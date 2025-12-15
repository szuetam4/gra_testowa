using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NewPlayer : MonoBehaviour
{
    private Vector3 pointerInput, movementInput;

    private GameObject player;

    public Rigidbody rb;

    public Transform orientation;

    private Vector3 moveDirection;

    public GameObject groundCollider;

    private groundColliderScript gr_script;

    private bool isJumpPressed = false, canDash = true;

    private float isDashPressed, isLMBPressed;

    private bool doubleJump = false;

    private bool inJump = false;

    private float speed, maxSpeed = 12;

    private float inputX, yRotation;

    [SerializeField]
    private InputActionReference movement, pointerPosition, dash, LMB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("SecondPlayer");
        groundCollider = GameObject.FindGameObjectWithTag("groundCollider");
        rb = GetComponent<Rigidbody>();
        gr_script = groundCollider.GetComponent<groundColliderScript>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();
        speed = rb.linearVelocity.magnitude;
        isJumpPressed = Input.GetButtonDown("Jump");
        isDashPressed = dash.action.ReadValue<float>();
        isLMBPressed = LMB.action.ReadValue<float>();

        movementInput = movement.action.ReadValue<Vector2>();

        if (!inJump)
        {
            movementInput.z = 0;
        }

        if (isJumpPressed && gr_script.PlayerTouchesGround)
        {
            Jump();
        }
        if (gr_script.PlayerTouchesGround)
        {
            rb.linearDamping = 6;
        }
        else
        {
            rb.linearDamping = 0;
        }
        if (!gr_script.PlayerTouchesGround && doubleJump && isJumpPressed)
        {
            DoubleJump();
            doubleJump = false;
        }
        if (isDashPressed == 1 && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
        Debug.Log(isLMBPressed);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * 60, ForceMode.Impulse);
        inJump = !inJump;
        doubleJump = true;
    }

    private void DoubleJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 40, rb.linearVelocity.z);
    }
    
    IEnumerator DashCoroutine()
    {
        canDash = false;
        maxSpeed = 100;
        rb.AddForce(moveDirection.normalized * 250f, ForceMode.Impulse);
        rb.AddForce(transform.up * 13f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        maxSpeed = 12;
        yield return new WaitForSeconds(4.9f);
        canDash = true;
        Debug.Log("Can dash");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        //Debug.Log(speed);
        rb.AddForce(moveDirection.normalized * 250f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
}
