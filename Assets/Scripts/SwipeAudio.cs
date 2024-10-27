using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeAudio : MonoBehaviour
{
    public AudioSource swipeAudioSource; // ���������� AudioSource ����� Inspector
    private TouchControls touchControls;
    private Vector2 startPosition;
    private bool isSwiping = false;

    private void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
        touchControls.Touch.PrimaryContact.started += StartSwipe;
        touchControls.Touch.PrimaryContact.canceled += EndSwipe;
        touchControls.Touch.PrimaryPosition.performed += TrackSwipe;
    }

    private void OnDisable()
    {
        touchControls.Touch.PrimaryContact.started -= StartSwipe;
        touchControls.Touch.PrimaryContact.canceled -= EndSwipe;
        touchControls.Touch.PrimaryPosition.performed -= TrackSwipe;
        touchControls.Disable();
    }

    private void StartSwipe(InputAction.CallbackContext context)
    {
        // ������ ������ - ��������� ��������� �������
        startPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        isSwiping = true;
    }

    private void TrackSwipe(InputAction.CallbackContext context)
    {
        if (!isSwiping) return;

        Vector2 currentPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float swipeDistance = currentPosition.x - startPosition.x;

        // �������� �� ����� ����� ��� ������
        if (Mathf.Abs(swipeDistance) > 25f) // ��������� �������� ��� ������������� ������
        {
            // ���������, ��������������� �� ���� � �������� �� ���� � SoundManager
            if (swipeAudioSource != null && !swipeAudioSource.isPlaying && !GetSoundManagerInstance().IsMuted)
            {
                swipeAudioSource.Play(); // ������������� ����, ���� �� �� ������ � �� ��������
            }
            isSwiping = false; // ��������� �����
        }
    }

    private void EndSwipe(InputAction.CallbackContext context)
    {
        isSwiping = false;
    }

    // ��������������� ����� ��� ��������� ���������� SoundManager
    private SoundManager GetSoundManagerInstance()
    {
        return FindObjectOfType<SoundManager>(); // ������� SoundManager � �����
    }
}
