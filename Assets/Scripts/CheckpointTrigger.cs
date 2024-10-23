using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex; // Индекс этого чекпоинта
    public CheckpointSystem checkpointSystem; // Ссылка на CheckpointSystem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверка на игрока
        {
            // Сообщаем системе чекпоинтов о пересечении
            checkpointSystem.SaveCheckpoint(checkpointIndex);
            Debug.Log("Player reached checkpoint: " + checkpointIndex); // Лог для отладки
        }
    }
}
