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

    private bool isJumpPressed = false;

    private bool inJump = false;

    private float speed;

    private float inputX, yRotation;

    [SerializeField]
    private InputActionReference movement, pointerPosition;

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

        movementInput = movement.action.ReadValue<Vector2>();

        if (!inJump)
        {
            movementInput.z = 0;
        }

        if (isJumpPressed && gr_script.PlayerTouchesGround)
        {
            inJump = true;
        }
        if (gr_script.PlayerTouchesGround)
        {
            rb.linearDamping = 6;
        }
        else
        {
            rb.linearDamping = 0;
        }
        
    }

    private void FixedUpdate()
    {
        if (inJump)
        {
            rb.AddForce(Vector3.up * 60, ForceMode.Impulse);
            inJump = !inJump;
        }
        MovePlayer();
        
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        Debug.Log(speed);
        rb.AddForce(moveDirection.normalized * 250f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > 12)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * 12;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
}
