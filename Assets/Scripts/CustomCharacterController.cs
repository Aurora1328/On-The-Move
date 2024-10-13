using System;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    public InputManager inputManager;   // Reference to the InputManager
    public float jumpForce = 10f;       // Jump force
    public float rotationSpeed = 5f;    // Speed of rotation
    private Rigidbody rb;               // Reference to the Rigidbody component
    private int jumpCount = 0;          // Counter for the number of jumps
    public int maxJumps = 1;            // Maximum number of jumps allowed
    public LayerMask groundLayer;       // LayerMask for identifying ground

    public float speed = 5f;            // Constant movement speed

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        inputManager.OnJump += PerformJump;  // Listen for jump events
        inputManager.OnSwipe += RotateCharacter;  // Listen for swipe events
    }

    private void OnDisable()
    {
        inputManager.OnJump -= PerformJump;
        inputManager.OnSwipe -= RotateCharacter;
    }

    private void Update()
    {
        // Move the character forward based on its current facing direction (local forward direction)
        MoveCharacter();
    }

    // Move the character at a constant speed in the forward direction
    private void MoveCharacter()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime; // Move in the direction the character is facing
        rb.MovePosition(rb.position + move); // Use Rigidbody to move the character
    }

    // Perform jump action
    private void PerformJump()
    {
        if (IsGrounded() || jumpCount < maxJumps)  // Allow jumping if grounded or if jump count is less than max
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity to avoid bouncing
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;  // Increment jump count
        }
    }

    // Rotate character based on swipe
    private void RotateCharacter(Vector2 swipeDirection)
    {
        // Calculate rotation direction based on swipe
        if (swipeDirection.x > 0)
        {
            // Swipe right
            transform.Rotate(0, 90, 0);
        }
        else if (swipeDirection.x < 0)
        {
            // Swipe left
            transform.Rotate(0, -90, 0);
        }
    }


    // Check if the character is grounded
    private bool IsGrounded()
    {
        // Check if the character is touching the ground using a raycast
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    // Reset jump count when the character lands
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ensure the object has the "Ground" tag
        {
            jumpCount = 0; // Reset jump count when landing
        }
    }
}
