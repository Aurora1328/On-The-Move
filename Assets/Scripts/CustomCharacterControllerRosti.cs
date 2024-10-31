using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Необходим для использования EventSystem
using System.Collections;

public class CustomCharacterControllerRosti : MonoBehaviour
{
    public InputManager inputManager;
    public Button buttonToHide;
    public Button startButton;
    public float jumpForce = 10f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    private int jumpCount = 0;
    public int maxJumps = 1;
    public LayerMask groundLayer;

    public float speed = 5f;
    public float speedIncrement = 0.1f; // Увеличение скорости за секунду
    public float maxSpeed = 15f; // Максимальная скорость
    public float fallThreshold = -1f;

    public Restart restartManager;
    public GameUIManager uiManager;
    public Animator animator;

    [SerializeField] // Делает переменную видимой в инспекторе
    public bool isGameOver = false;

    private bool isJumping = false;
    private bool isGameStarted = false;

    // Для броска еды
    public GameObject[] foodPrefabs;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float throwDelay = 1f;

    private bool canThrow = true;

    public InfiniteRoadMenu infiniteRoadMenu;
    public JumpSoundManager jumpSoundManager; // Ссылка на скрипт JumpSoundManager

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to " + gameObject.name);
        }
    }

    private void Start()
    {
        buttonToHide.gameObject.SetActive(false);
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnEnable()
    {
        inputManager.OnJump += PerformJump;
        inputManager.OnSwipe += RotateCharacter;
        inputManager.OnFeed += ThrowFood;
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
            // Увеличиваем скорость со временем
            IncreaseSpeedOverTime();
            MoveCharacter();
            CheckIfFellOutOfZone();
        }
    }

    private void IncreaseSpeedOverTime()
    {
        // Увеличиваем скорость, но не превышаем максимальное значение
        speed = Mathf.Min(speed + speedIncrement * Time.deltaTime, maxSpeed);
    }

    private void MoveCharacter()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void PerformJump()
    {
        // Проверяем, не нажата ли UI кнопка броска еды, чтобы исключить прыжок при её нажатии
        if (isGameOver || isJumping || !isGameStarted || animator.GetBool("isFeeding"))
            return;

        // Проверяем, нажата ли какая-либо UI кнопка, чтобы избежать случайных прыжков от любых касаний
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            return; // Если любая кнопка UI нажата, прыжок не выполняется
        }

        // Выполняем прыжок, если персонаж на земле или имеет ещё прыжки
        if (IsGrounded() || jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            isJumping = true;
            animator.SetTrigger("Jump");
            animator.SetBool("IsGrounded", false);

            // Воспроизводим звук прыжка
            jumpSoundManager.PlayJumpSound();
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

        CenterCharacterOnRoad();
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
            isJumping = false;
            animator.ResetTrigger("Jump");
            animator.SetBool("IsGrounded", true);
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
        speed = 5f; // Сброс скорости при перезапуске
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(3.3f, 0.2f, -5.59f);
    }

    public void StartGame()
    {
        isGameStarted = true;
        buttonToHide.gameObject.SetActive(true);
        rb.isKinematic = false; // Отключаем kinematic для включения управления

        infiniteRoadMenu.startButton.SetActive(false);
        infiniteRoadMenu.musicOnButton.SetActive(false);
        infiniteRoadMenu.musicOffButton.SetActive(false);
        infiniteRoadMenu.levelsButton.SetActive(false);
        infiniteRoadMenu.soundOnButton.SetActive(false);
        infiniteRoadMenu.soundOffButton.SetActive(false);
        infiniteRoadMenu.label.SetActive(false);
    }

    private void OnStartButtonClicked()
    {
        StartGame();
    }

    public void ThrowFood()
    {
        if (isGameOver || !canThrow) return;

        Debug.Log("Feed button pressed, throwing food!");

        animator.SetBool("isFeeding", true); // Устанавливаем isFeeding в true

        // Бросаем еду без блокировки прыжков
        StartCoroutine(ThrowFoodCoroutine());

        // Сбрасываем возможность броска через 0.1 сек.
        canThrow = false;
        Invoke(nameof(ResetThrow), 0.1f);
    }

    private IEnumerator ThrowFoodCoroutine()
    {
        int randomIndex = Random.Range(0, foodPrefabs.Length);

        Quaternion rotation = Quaternion.Euler(90, 90, 0);

        GameObject food = Instantiate(foodPrefabs[randomIndex], throwPoint.position + Vector3.up * 0.5f, rotation);

        Rigidbody foodRb = food.GetComponent<Rigidbody>();
        foodRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        yield return null;  // Возвращаем управление сразу

        animator.SetBool("isFeeding", false); // Сбрасываем isFeeding в false после броска еды
    }

    private void ResetThrow()
    {
        canThrow = true;
    }

    private void CenterCharacterOnRoad()
    {
        Collider[] roadColliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 1f, 0.5f), Quaternion.identity);

        foreach (Collider collider in roadColliders)
        {
            if (collider.CompareTag("Road"))
            {
                Vector3 centerPosition = collider.bounds.center;
                centerPosition.y = transform.position.y;
                transform.position = centerPosition;
                break;
            }
        }
    }

    public void StartGameFromLevel()
    {
        ResetCharacter();  // Сбрасываем позицию и статус персонажа
        StartGame();       // Запускаем игру
    }

    // Свойство для отображения статуса игры
    public bool IsGameOver
    {
        get { return isGameOver; }
    }
}
