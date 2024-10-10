using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    public InputManager inputManager;   // Reference to the InputManager
    public float jumpForce = 10f;       // Jump force
    public float rotationSpeed = 5f;     // Speed of rotation
    private Rigidbody rb;                // Reference to the Rigidbody component
    private int jumpCount = 0;           // Counter for the number of jumps
    public int maxJumps = 1;             // Maximum number of jumps allowed
    public LayerMask groundLayer;        // LayerMask for identifying ground

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

    // Rotate character by 90 degrees
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
        // If you want to implement up/down swipe rotation, you can add:
        // if (swipeDirection.y > 0) { ... } // Implement if needed
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
