using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.8f; // Gravity slightly reduced for smoother falling
    public float jumpHeight = 1f;
    public float fallMultiplier = 10f; // A multiplier to speed up falling smoothly

    public float groundDistance = 0.4f;
    public LayerMask groundMask = ~0; // GroundMask is set to everything

    private CharacterController controller;
    private Transform groundCheck;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canJump = true; // Player can initially jump

    void Start()
    {
        // Finding the "Player" object for the controller
        controller = GameObject.Find("Player").GetComponent<CharacterController>();

        // Finding the "Floor" object for the groundCheck
        groundCheck = GameObject.Find("Floor").transform;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset the velocity when grounded to prevent further downward momentum
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f; // Slight downward velocity to help stick to the ground smoothly
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Movement based on player input
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping logic: Only allow jumping if the player can jump and is grounded
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Apply jump force
            canJump = false; // Disable jumping until player hits the floor
        }

        // Apply gravity: If falling, apply extra gravity for smoother fall
        if (velocity.y < 0)
        {
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime; // Smooth falling acceleration
        }

        // Always apply gravity to pull the player down
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Detect collision with the "Floor" to allow jumping again
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Floor")
        {
            canJump = true; // Allow jumping again when the player collides with the "Floor"
        }
    }
}
