using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void OnJumpAction();
    public event OnJumpAction OnJump;

    public delegate void OnSwipeAction(Vector2 direction);
    public event OnSwipeAction OnSwipe;

    public delegate void OnFeedAction();
    public event OnFeedAction OnFeed;

    private Vector2 startPosition;
    private float startTime;

    [SerializeField] private float minimalSwipeDistance = 100f;
    [SerializeField] private float maximumSwipeTime = 0.5f;

    [SerializeField] private Button feedButton;
    private bool isFeeding = false;

    private void Awake()
    {
        touchControls = new TouchControls();

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
        else if (swipeDistance < minimalSwipeDistance && !isFeeding)
        {
            OnJump?.Invoke();
        }
    }

    public void FeedButtonPressed()
    {
        isFeeding = true;
        OnFeed?.Invoke();
        Debug.Log("Feed button pressed, throwing food!");

        StartCoroutine(ResetFeedingFlag());
    }

    private IEnumerator ResetFeedingFlag()
    {
        yield return new WaitForSeconds(0.1f);
        isFeeding = false;
    }
}
