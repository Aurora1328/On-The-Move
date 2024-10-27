using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonClickSound; // Установите AudioSource для звука кнопки в Inspector

    private void Start()
    {
        // Убедитесь, что у нас есть компонент Button на этом объекте
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // Добавляем обработчик нажатия на кнопку
        }
    }

    public void OnButtonClick()
    {
        // Воспроизводим звук кнопки
        if (buttonClickSound != null)
        {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlaySound(buttonClickSound); // Воспроизводим звук кнопки
            }
            else
            {
                Debug.LogWarning("SoundManager не найден!");
            }
        }
    }
}
