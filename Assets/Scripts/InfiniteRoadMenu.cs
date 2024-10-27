using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Подключаем для работы с сценами

public class InfiniteRoadMenu : MonoBehaviour
{
    public GameObject[] roadSegments;
    public float speed = 5f;
    public float segmentLength = 2.95f;

    public Transform player;
    public bool isGameStarted = false;

    private Vector3[] initialPositions;

    public GameObject[] restartPanels;
    public GameObject[] menuPanels; // Массив для хранения нескольких панелей главного меню
    public GameObject startButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject levelsButton;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject levelsCanvas;
    public GameObject label;

    public CheckpointSystem checkpointSystem; // Ссылка на CheckpointSystem для телепортации на чекпоинты

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    private void Start()
    {
        initialPositions = new Vector3[roadSegments.Length];
        for (int i = 0; i < roadSegments.Length; i++)
        {
            initialPositions[i] = roadSegments[i].transform.position;
        }

        UpdateMusicButtons();
        UpdateSoundButtons();
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            MoveRoadSegments();
        }
    }

    private void MoveRoadSegments()
    {
        for (int i = 0; i < roadSegments.Length; i++)
        {
            roadSegments[i].transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Проверяем, когда сегмент уходит за пределы экрана
            if (roadSegments[i].transform.position.x < initialPositions[0].x - segmentLength)
            {
                // Находим последний сегмент
                int lastIndex = (i - 1 + roadSegments.Length) % roadSegments.Length;

                // Вычисляем новую позицию для текущего сегмента за последним
                float newX = roadSegments[lastIndex].transform.position.x + segmentLength;
                roadSegments[i].transform.position = new Vector3(newX, initialPositions[i].y, initialPositions[i].z);
            }
        }
    }

    public void OnStartButtonPressed()
    {
        isGameStarted = true;
        ResetRoadSegments();

        startButton.SetActive(false);
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(false);
        levelsButton.SetActive(false);
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(false);
        label.SetActive(false);

        this.enabled = false;
    }

    private void ResetRoadSegments()
    {
        for (int i = 0; i < roadSegments.Length; i++)
        {
            roadSegments[i].transform.position = initialPositions[i];
        }
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        UpdateMusicButtons();
    }

    private void UpdateMusicButtons()
    {
        musicOnButton.SetActive(isMusicOn);
        musicOffButton.SetActive(!isMusicOn);
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        UpdateSoundButtons();
    }

    private void UpdateSoundButtons()
    {
        soundOnButton.SetActive(isSoundOn);
        soundOffButton.SetActive(!isSoundOn);
    }

    public void OnLevelsButtonPressed()
    {
        levelsCanvas.SetActive(true); // Включаем Canvas для уровней
        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false); // Отключаем все панели главного меню
        }
        isGameStarted = false; // Устанавливаем isGameStarted в false
    }

    public void OnLevelsButton()
    {
        levelsCanvas.SetActive(true); // Включаем Canvas для уровней
        foreach (GameObject restartPanel in restartPanels)
        {
            restartPanel.SetActive(false);
        }
    }

    public void ShowRestartPanel()
    {
        foreach (GameObject restartPanel in restartPanels)
        {
            restartPanel.SetActive(true); // Показываем панели
        }
    }

    public void OnBackButtonPressed()
    {
        levelsCanvas.SetActive(false); // Отключаем Canvas для уровней
        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(true); // Включаем все панели главного меню
        }
    }

    // Метод для запуска игры с выбранного уровня (чекпоинта)
    public void OnLevelSelected(int checkpointIndex)
    {
        // Скрываем Canvas уровней
        levelsCanvas.SetActive(false);

        // Запускаем игру
        isGameStarted = true;

        // Останавливаем движение дороги
        this.enabled = false;

        // Сбрасываем положение сегментов дороги
        ResetRoadSegments();

        // Телепортируем игрока к выбранному чекпоинту
        checkpointSystem.TeleportToCheckpoint(checkpointIndex);
    }

    // Методы для кнопок уровней
    public void OnLevel1ButtonPressed() { OnLevelSelected(0); } // Начало с первого чекпоинта
    public void OnLevel2ButtonPressed() { OnLevelSelected(1); } // Начало со второго чекпоинта
    public void OnLevel3ButtonPressed() { OnLevelSelected(2); } // Начало с третьего чекпоинта
    public void OnLevel4ButtonPressed() { OnLevelSelected(3); } // Начало с четвертого чекпоинта
    public void OnLevel5ButtonPressed() { OnLevelSelected(4); } // Начало с пятого чекпоинта
    public void OnLevel6ButtonPressed() { OnLevelSelected(5); } // Начало с шестого чекпоинта
    public void OnLevel7ButtonPressed() { OnLevelSelected(6); } // Начало с седьмого чекпоинта
    public void OnLevel8ButtonPressed() { OnLevelSelected(7); } // Начало с восьмого чекпоинта
    public void OnLevel9ButtonPressed() { OnLevelSelected(8); } // Начало с девятого чекпоинта

    // Метод для перезагрузки текущей сцены
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Метод, который можно вызвать для перезапуска игры
    public void OnRestartButtonPressed()
    {
        RestartGame();
    }
}
