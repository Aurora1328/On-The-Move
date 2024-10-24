using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    // Массив всех чекпоинтов, используемых в игре для сохранения прогресса
    public Transform[] checkpoints;

    private int currentCheckpointIndex = 0; // Индекс текущего чекпоинта
    private Transform player; // Ссылка на игрока

    private void Start()
    {
        // Находим игрока в сцене
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Изначально телепортируем игрока на 0-й чекпоинт БЕЗ СОХРАНЕНИЯ прогресса
        TeleportToCheckpoint(0, saveProgress: false);
    }

    // Метод для сохранения чекпоинта
    public void SaveCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex > 0)
        {
            Debug.Log("Saving checkpoint: " + checkpointIndex);
            PlayerPrefs.SetInt("Checkpoint", checkpointIndex);
            PlayerPrefs.Save(); // Сохраняем PlayerPrefs
        }
        else
        {
            Debug.Log("Checkpoint 0, progress not saved.");
        }

        currentCheckpointIndex = checkpointIndex;
    }

    // Метод для загрузки последнего сохранённого чекпоинта
    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            currentCheckpointIndex = PlayerPrefs.GetInt("Checkpoint");
            Debug.Log("Loaded checkpoint: " + currentCheckpointIndex);
        }
        else
        {
            currentCheckpointIndex = 0; // Если данных нет, начинаем с первого чекпоинта
            Debug.Log("No saved checkpoint found, starting from the first one.");
        }

        TeleportToCheckpoint(currentCheckpointIndex);
    }

    // Метод для перемещения игрока к чекпоинту
    public void TeleportToCheckpoint(int checkpointIndex, bool saveProgress = true)
    {
        if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length)
        {
            Transform checkpointTransform = checkpoints[checkpointIndex];
            player.position = checkpointTransform.position;
            player.rotation = checkpointTransform.rotation;
            Debug.Log("Teleporting player to checkpoint: " + checkpointIndex);

            if (saveProgress)
            {
                SaveCheckpoint(checkpointIndex);
            }
        }
        else
        {
            Debug.LogError("Checkpoint index is out of bounds: " + checkpointIndex);
        }
    }

    // Метод для кнопки "Старт", который начинает игру и телепортирует на сохранённый чекпоинт
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadCheckpoint();
    }

    // Пример метода для сохранения текущего чекпоинта, если это нужно
    public void SetCurrentCheckpoint(int checkpointIndex)
    {
        SaveCheckpoint(checkpointIndex);
    }
}
