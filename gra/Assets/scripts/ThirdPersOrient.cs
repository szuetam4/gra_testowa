using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersOrient : MonoBehaviour
{
    private Vector3 movementInput;
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [SerializeField]
    private InputActionReference movement;

    public float rotationSpeed;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        Vector3 inputDir = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
