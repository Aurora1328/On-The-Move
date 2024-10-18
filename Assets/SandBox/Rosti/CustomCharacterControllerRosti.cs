using UnityEngine;
using System.Collections;

public class CustomCharacterControllerRosti : MonoBehaviour
{
    public InputManager inputManager; // Ссылка на InputManager
    public float jumpForce = 10f;
    public float rotationSpeed = 5f;
    public float speed = 5f;
    public float fallThreshold = -1f;

    private Rigidbody rb;
    private int jumpCount = 0;
    public int maxJumps = 1;
    public LayerMask groundLayer;

    public Restart restartManager;
    public GameUIManager uiManager;
    public Animator animator;

    private bool isGameOver = false;
<<<<<<< HEAD
    private bool isJumping = false;

    // Для броска еды
    public GameObject[] foodPrefabs;  // Массив префабов еды
    public Transform throwPoint;       // Точка, откуда бросать еду
    public float throwForce = 10f;     // Сила броска
    public float throwDelay = 1f;      // Задержка между бросками

    private bool canThrow = true; // Флаг, указывающий, может ли персонаж бросить еду
=======
    private bool isJumping = false;  
    private bool isGameStarted = false; 
>>>>>>> origin/СreatedAnEndlessRoadInTheMainMenu

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
        inputManager.OnJump += PerformJump;      // Подписка на событие прыжка
        inputManager.OnSwipe += RotateCharacter;  // Подписка на событие поворота
        inputManager.OnFeed += ThrowFood;         // Подписка на событие броска еды
    }

    private void OnDisable()
    {
        inputManager.OnJump -= PerformJump;
        inputManager.OnSwipe -= RotateCharacter;
        inputManager.OnFeed -= ThrowFood;
    }

    private void Update()
    {
        if (isGameStarted && !isGameOver)
        {
            MoveCharacter();
            CheckIfFellOutOfZone();
        }
    }

    private void MoveCharacter()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void PerformJump()
    {
<<<<<<< HEAD
        if (isGameOver || isJumping) return;
=======
        if (isGameOver || isJumping || !isGameStarted) return;  
>>>>>>> origin/СreatedAnEndlessRoadInTheMainMenu

        if (IsGrounded() || jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

<<<<<<< HEAD
            isJumping = true;
            animator.SetTrigger("Jump");
            animator.SetBool("IsGrounded", false);
=======
            isJumping = true;  
            animator.SetTrigger("Jump");  
            animator.SetBool("IsGrounded", false); 
>>>>>>> origin/СreatedAnEndlessRoadInTheMainMenu
        }
    }

    private void RotateCharacter(Vector2 swipeDirection)
    {
        if (isGameOver || !isGameStarted) return;  

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
<<<<<<< HEAD
            isJumping = false;
            animator.ResetTrigger("Jump");
            animator.SetBool("IsGrounded", true);
=======
            isJumping = false;  
            animator.ResetTrigger("Jump");  
            animator.SetBool("IsGrounded", true);  
>>>>>>> origin/СreatedAnEndlessRoadInTheMainMenu
        }
    }

    private void CheckIfFellOutOfZone()
    {
        if (transform.position.y < fallThreshold)
        {
            EndGame();
        }
    }

    public void EndGame()
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

<<<<<<< HEAD
    // Новый метод для броска еды
    public void ThrowFood()
    {
        if (isGameOver || !canThrow) return;

        StartCoroutine(ThrowFoodCoroutine());
    }

    private IEnumerator ThrowFoodCoroutine()
    {
        canThrow = false; // Бросок еды сейчас запрещен

        // Выбираем случайный префаб еды
        int randomIndex = Random.Range(0, foodPrefabs.Length);

        // Поворачиваем еду на 90 градусов по оси Z
        Quaternion rotation = Quaternion.Euler(90, 90, 0);

        // Создаем еду с учетом поворота
        GameObject food = Instantiate(foodPrefabs[randomIndex], throwPoint.position + Vector3.up * 0.5f, rotation);

        Rigidbody foodRb = food.GetComponent<Rigidbody>();
        foodRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        yield return new WaitForSeconds(throwDelay); // Ждем задержку
        canThrow = true; // Теперь бросок еды разрешен
    }
}
=======
    public void StartGame()
    {
        isGameStarted = true;
    }
}
>>>>>>> origin/СreatedAnEndlessRoadInTheMainMenu
