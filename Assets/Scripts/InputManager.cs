using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void OnJumpAction();
    public event OnJumpAction OnJump;

    public delegate void OnSwipeAction(Vector2 direction);
    public event OnSwipeAction OnSwipe;

    private Vector2 startPosition;
    private float startTime;

    [SerializeField] private float minimalSwipeDistance = 100f;
    [SerializeField] private float maximumSwipeTime = 0.5f;

    private void Awake()
    {
        touchControls = new TouchControls();
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
        Debug.Log($"Touch Started at Position: {startPosition}");
    }

    private void TouchEnded(InputAction.CallbackContext context)
    {
        Vector2 endPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float endTime = Time.time;

        Vector2 swipeDirection = endPosition - startPosition;
        float swipeDistance = swipeDirection.magnitude;
        float swipeDuration = endTime - startTime;

        Debug.Log($"Touch Ended. Start Position: {startPosition}, End Position: {endPosition}");
        Debug.Log($"Swipe Distance: {swipeDistance}, Swipe Duration: {swipeDuration}");

        if (swipeDistance >= minimalSwipeDistance && swipeDuration <= maximumSwipeTime)
        {
            OnSwipe?.Invoke(swipeDirection.normalized);
        }
        else if (swipeDistance < minimalSwipeDistance)
        {
            Debug.Log("Jump action triggered.");
            OnJump?.Invoke();
        }
    }
}
