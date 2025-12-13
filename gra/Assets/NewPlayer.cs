using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NewPlayer : MonoBehaviour
{
    private Vector3 pointerInput, movementInput;

    public Rigidbody rb;

    public GameObject groundCollider;

    private groundColliderScript gr_script;

    private bool isJumpPressed = false;

    private bool inJump = false;

    [SerializeField]
    private InputActionReference movement, pointerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundCollider = GameObject.FindGameObjectWithTag("groundCollider");
        rb = GetComponent<Rigidbody>();
        gr_script = groundCollider.GetComponent<groundColliderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        isJumpPressed = Input.GetButtonDown("Jump");

        pointerInput = GetPointerInput();

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

    private Vector2 GetPointerInput()
    {
        Vector2 mousePos = pointerPosition.action.ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void FixedUpdate() {
        if (inJump)
        {
            rb.AddForce(Vector3.up*60, ForceMode.VelocityChange);
            inJump = !inJump;
        }
        rb.linearVelocity = new Vector3(movementInput.x*20, rb.linearVelocity.y, movementInput.y*20);
        Debug.Log(gr_script.PlayerTouchesGround);
    }
}
