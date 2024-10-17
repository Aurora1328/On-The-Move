using UnityEngine;

public class CustomCharacterControllerRosti : MonoBehaviour
{
    public InputManager inputManager;
    public float jumpForce = 10f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    private int jumpCount = 0;
    public int maxJumps = 1;
    public LayerMask groundLayer;

    public float speed = 5f;
    public float fallThreshold = -1f;

    public Restart restartManager;
    public GameUIManager uiManager;
    public Animator animator;

    private bool isGameOver = false;
    private bool isJumping = false; // Новый флаг для отслеживания прыжка

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
        inputManager.OnJump += PerformJump;
        inputManager.OnSwipe += RotateCharacter;
    }

    private void OnDisable()
    {
        inputManager.OnJump -= PerformJump;
        inputManager.OnSwipe -= RotateCharacter;
    }

    private void Update()
    {
        MoveCharacter();
        CheckIfFellOutOfZone();
    }

    private void MoveCharacter()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void PerformJump()
    {
        if (isGameOver || isJumping) return; // Не позволяем прыгать, если уже в прыжке

        if (IsGrounded() || jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            isJumping = true;  // Флаг для отслеживания прыжка
            animator.SetTrigger("Jump");  // Включаем анимацию прыжка
            animator.SetBool("IsGrounded", false);  // Устанавливаем флаг IsGrounded в false
        }
    }

    private void RotateCharacter(Vector2 swipeDirection)
    {
        if (isGameOver) return;

        if (swipeDirection.x > 0)
        {
            transform.Rotate(0, 90, 0);
        }
        else if (swipeDirection.x < 0)
        {
            transform.Rotate(0, -90, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isJumping = false;  // Персонаж приземлился, можно прыгать снова
            animator.ResetTrigger("Jump");  // Сбрасываем триггер прыжка
            animator.SetBool("IsGrounded", true);  // Устанавливаем флаг IsGrounded в true
        }
    }

    private void CheckIfFellOutOfZone()
    {
        if (transform.position.y < fallThreshold)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        restartManager.ShowGameOverScreen();
    }

    public void ResetCharacter()
    {
        isGameOver = false;
        jumpCount = 0;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(3.3f, 0.2f, -5.59f);
    }
}
