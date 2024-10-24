using UnityEngine;
using UnityEngine.UI;

public class InfiniteRoadMenu : MonoBehaviour
{
    public GameObject[] roadSegments;
    public float speed = 5f;
    public float segmentLength = 2.95f;

    public Transform player;
    public bool isGameStarted = false;

    private Vector3[] initialPositions;

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
        foreach (GameObject segment in roadSegments)
        {
            segment.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (segment.transform.position.x < initialPositions[0].x - segmentLength)
            {
                float newX = initialPositions[0].x + (segmentLength * (roadSegments.Length - 1));
                segment.transform.position = new Vector3(newX, initialPositions[0].y, initialPositions[0].z);
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

        // Любые другие действия, необходимые для начала игры
    }

    // Методы для кнопок уровней
    public void OnLevel1ButtonPressed()
    {
        OnLevelSelected(0); // Начало с первого чекпоинта (первый уровень)
    }

    public void OnLevel2ButtonPressed()
    {
        OnLevelSelected(1); // Начало со второго чекпоинта (второй уровень)
    }

    public void OnLevel3ButtonPressed()
    {
        OnLevelSelected(2); // Начало с третьего чекпоинта (третий уровень)
    }

    public void OnLevel4ButtonPressed()
    {
        OnLevelSelected(3); // Начало с четвертого чекпоинта (четвертый уровень)
    }

    public void OnLevel5ButtonPressed()
    {
        OnLevelSelected(4); // Начало с пятого чекпоинта (пятый уровень)
    }

    public void OnLevel6ButtonPressed()
    {
        OnLevelSelected(5); // Начало с шестого чекпоинта (шестой уровень)
    }

    public void OnLevel7ButtonPressed()
    {
        OnLevelSelected(6); // Начало с седьмого чекпоинта (седьмой уровень)
    }

    public void OnLevel8ButtonPressed()
    {
        OnLevelSelected(7); // Начало с восьмого чекпоинта (восьмой уровень)
    }

    public void OnLevel9ButtonPressed()
    {
        OnLevelSelected(8); // Начало с девятого чекпоинта (девятый уровень)
    }
}
