using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeAudio : MonoBehaviour
{
    public AudioSource swipeAudioSource; // Установите AudioSource через Inspector
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
        // Начало свайпа - фиксируем начальную позицию
        startPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        isSwiping = true;
    }

    private void TrackSwipe(InputAction.CallbackContext context)
    {
        if (!isSwiping) return;

        Vector2 currentPosition = touchControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float swipeDistance = currentPosition.x - startPosition.x;

        // Проверка на свайп влево или вправо
        if (Mathf.Abs(swipeDistance) > 25f) // Пороговое значение для распознавания свайпа
        {
            // Проверяем, воспроизводится ли звук и отключен ли звук в SoundManager
            if (swipeAudioSource != null && !swipeAudioSource.isPlaying && !GetSoundManagerInstance().IsMuted)
            {
                swipeAudioSource.Play(); // Воспроизводим звук, если он не играет и не отключен
            }
            isSwiping = false; // Завершаем свайп
        }
    }

    private void EndSwipe(InputAction.CallbackContext context)
    {
        isSwiping = false;
    }

    // Вспомогательный метод для получения экземпляра SoundManager
    private SoundManager GetSoundManagerInstance()
    {
        return FindObjectOfType<SoundManager>(); // Находим SoundManager в сцене
    }
}
