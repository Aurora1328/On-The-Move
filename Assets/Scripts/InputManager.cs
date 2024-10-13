using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void OnJumpAction();
    public event OnJumpAction OnJump; // Event for jump action

    public delegate void OnSwipeAction(Vector2 direction);
    public event OnSwipeAction OnSwipe; // Event for swipe action

    private Vector2 startPosition;
    private float startTime;

    // Minimal swipe distance and max swipe time (you can tweak these values)
    [SerializeField] private float minimalSwipeDistance = 100f; // Pixels
    [SerializeField] private float maximumSwipeTime = 0.5f;     // Seconds

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
        // Register touch start and end events
        touchControls.Touch.PrimaryContact.started += ctx => TouchStarted(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => TouchEnded(ctx);
    }

    // Handle touch start - track position and time
    private void TouchStarted(InputAction.CallbackContext context)
    {
        startPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        startTime = (float)context.startTime;

        Debug.Log($"Touch Started at Position: {startPosition}");
    }

    // Handle touch end - detect if it's a tap or swipe
    private void TouchEnded(InputAction.CallbackContext context)
    {
        Vector2 endPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float endTime = (float)context.time;

        Vector2 swipeDirection = endPosition - startPosition;
        float swipeDistance = swipeDirection.magnitude;
        float swipeDuration = endTime - startTime;

        Debug.Log($"Touch Ended. Start Position: {startPosition}, End Position: {endPosition}");
        Debug.Log($"Swipe Distance: {swipeDistance}, Swipe Duration: {swipeDuration}");

        // Check if it's a swipe
        if (swipeDistance >= minimalSwipeDistance && swipeDuration <= maximumSwipeTime)
        {
            // This is a swipe
            OnSwipe?.Invoke(swipeDirection.normalized);
        }
        // If it's not a swipe, treat it as a tap (jump)
        else if (swipeDistance < minimalSwipeDistance)
        {
            Debug.Log("Jump action triggered.");
            OnJump?.Invoke(); // This is a tap, trigger jump immediately
        }
    }

}
