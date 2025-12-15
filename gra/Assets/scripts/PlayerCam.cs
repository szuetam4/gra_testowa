using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform orientation;

    private float yRotation;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 1500;

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
