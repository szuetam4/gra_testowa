using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayer : MonoBehaviour
{
    private Vector3 pointerInput, movementInput;

    public Rigidbody rb;

    private bool isJumpPressed = false;

    private bool inJump = false;

    [SerializeField]
    private InputActionReference movement, pointerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (isJumpPressed)
        {
            inJump = true;
            Debug.Log(isJumpPressed);
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
            movementInput.z = 200;
            inJump = !inJump;
        }
        rb.linearVelocity = new Vector3(movementInput.x*20, movementInput.z, movementInput.y*20);
    }
}
