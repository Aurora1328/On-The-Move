using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Необходим для работы с UI
using System.Collections;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void OnJumpAction();
    public event OnJumpAction OnJump;

    public delegate void OnSwipeAction(Vector2 direction);
    public event OnSwipeAction OnSwipe;

    public delegate void OnFeedAction();   // Новый делегат для события "Feed"
    public event OnFeedAction OnFeed;      // Новое событие для броска еды

    private Vector2 startPosition;
    private float startTime;

    [SerializeField] private float minimalSwipeDistance = 100f;
    [SerializeField] private float maximumSwipeTime = 0.5f;

    [SerializeField] private Button feedButton; // Ссылка на кнопку "Feed"
    private bool isFeeding = false; // Флаг, указывающий, что кнопка "Feed" нажата

    private void Awake()
    {
        touchControls = new TouchControls();

        // Подписываем кнопку на событие
        if (feedButton != null)
        {
            feedButton.onClick.AddListener(FeedButtonPressed);
        }
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => TouchStarted(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => TouchEnded(ctx);
    }

    private void TouchStarted(InputAction.CallbackContext context)
    {
        StartCoroutine(SetStartPositionWithDelay());
    }

    private IEnumerator SetStartPositionWithDelay()
    {
        yield return new WaitForEndOfFrame();
        startPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        startTime = Time.time;
    }

    private void TouchEnded(InputAction.CallbackContext context)
    {
        Vector2 endPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float endTime = Time.time;

        Vector2 swipeDirection = endPosition - startPosition;
        float swipeDistance = swipeDirection.magnitude;
        float swipeDuration = endTime - startTime;

        if (swipeDistance >= minimalSwipeDistance && swipeDuration <= maximumSwipeTime)
        {
            OnSwipe?.Invoke(swipeDirection.normalized);
        }
        else if (swipeDistance < minimalSwipeDistance && !isFeeding) // Игнорируем прыжок, если кнопка "Feed" нажата
        {
            OnJump?.Invoke(); // Триггер прыжка только если не было свайпа
        }
    }

    // Метод, вызываемый при нажатии кнопки "Feed"
    public void FeedButtonPressed()
    {
        isFeeding = true; // Устанавливаем флаг, что кнопка "Feed" нажата
        OnFeed?.Invoke();
        Debug.Log("Feed button pressed, throwing food!");

        // Сбрасываем флаг после броска еды
        StartCoroutine(ResetFeedingFlag());
    }

    private IEnumerator ResetFeedingFlag()
    {
        yield return new WaitForSeconds(0.1f); // Установите нужное время для защиты от быстрого повторного нажатия
        isFeeding = false; // Сбрасываем флаг
    }
}
