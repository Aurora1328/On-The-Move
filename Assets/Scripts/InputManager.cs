using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
    [SerializeField] private float minimalSwipeDistance = 0.2f;
    [SerializeField] private float maximumSwipeTime = 1f;

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
        // Tap input for jump
        touchControls.Touch.PrimaryContact.started += ctx => JumpTap();

        // Swipe detection
        touchControls.Touch.PrimaryContact.started += ctx => SwipeStart(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => SwipeEnd(ctx);
    }

    // Method to invoke the jump event when a tap is detected
    private void JumpTap()
    {
        if (OnJump != null)
        {
            OnJump.Invoke();
        }
    }

    // Swipe handling
    private void SwipeStart(InputAction.CallbackContext context)
    {
        startPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        startTime = (float)context.startTime;
    }

    private void SwipeEnd(InputAction.CallbackContext context)
    {
        Vector2 endPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float endTime = (float)context.time;

        Vector2 swipeDirection = endPosition - startPosition;

        // Check if swipe is long enough and within time
        if (swipeDirection.magnitude >= minimalSwipeDistance && (endTime - startTime) <= maximumSwipeTime)
        {
            OnSwipe?.Invoke(swipeDirection.normalized);  // Trigger swipe event with normalized direction
        }
    }
}
