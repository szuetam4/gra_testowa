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

    private float inputX, yRotation;

    [SerializeField]
    private InputActionReference movement, pointerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groundCollider = GameObject.FindGameObjectWithTag("groundCollider");
        rb = GetComponent<Rigidbody>();
        gr_script = groundCollider.GetComponent<groundColliderScript>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        if (inJump)
        {
            rb.AddForce(Vector3.up * 60, ForceMode.VelocityChange);
            inJump = !inJump;
        }
        MovePlayer();
        //rb.linearVelocity = new Vector3(movementInput.x * 20, rb.linearVelocity.y, movementInput.y * 20);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        rb.AddForce(moveDirection.normalized * 100f, ForceMode.Force);
    }
}
