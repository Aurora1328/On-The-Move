using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public bool[] unlockedCheckpoints; // Массив для хранения открытых чекпоинтов
    private int lastCheckpointIndex = -1; // Индекс последнего сохранённого чекпоинта

    private void Start()
    {
        // Инициализация массива открытых чекпоинтов
        if (unlockedCheckpoints == null || unlockedCheckpoints.Length == 0)
        {
            unlockedCheckpoints = new bool[9]; // 9 уровней
        }

        // По умолчанию открыт первый чекпоинт
        unlockedCheckpoints[0] = true;
    }

    // Метод для проверки, открыт ли чекпоинт
    public bool IsCheckpointUnlocked(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < unlockedCheckpoints.Length)
        {
            return unlockedCheckpoints[checkpointIndex];
        }
        else
        {
            Debug.LogWarning("Invalid checkpoint index: " + checkpointIndex);
            return false;
        }
    }

    // Метод для загрузки определённого чекпоинта
    public void LoadCheckpoint(int checkpointIndex)
    {
        if (IsCheckpointUnlocked(checkpointIndex))
        {
            Debug.Log("Checkpoint " + checkpointIndex + " loaded.");
            // Логика перемещения игрока к чекпоинту
        }
        else
        {
            Debug.LogError("Cannot load checkpoint " + checkpointIndex + ". It's not unlocked.");
        }
    }

    // Метод для открытия нового чекпоинта
    public void UnlockCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < unlockedCheckpoints.Length)
        {
            unlockedCheckpoints[checkpointIndex] = true;
            Debug.Log("Checkpoint " + checkpointIndex + " unlocked.");
        }
        else
        {
            Debug.LogWarning("Invalid checkpoint index: " + checkpointIndex);
        }
    }

    // Метод для сохранения текущего чекпоинта
    public void SaveCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < unlockedCheckpoints.Length)
        {
            lastCheckpointIndex = checkpointIndex;
            Debug.Log("Checkpoint " + checkpointIndex + " saved.");
        }
        else
        {
            Debug.LogWarning("Invalid checkpoint index: " + checkpointIndex);
        }
    }

    // Метод для получения последнего сохранённого чекпоинта
    public int GetLastCheckpoint()
    {
        return lastCheckpointIndex;
    }
}
