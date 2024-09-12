using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Control rotation around x axis (Look up and down)
        xRotation -= mouseY;

        // Clamp the rotation so we can't over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Control rotation around y axis (left-right)
        YRotation += mouseX;

        // Apply only Y-axis (left-right) rotation to the player body
        transform.localRotation = Quaternion.Euler(0f, YRotation, 0f);

        // Apply only X-axis (up-down) rotation to the camera (assuming the camera is a child of the player object)
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
