using UnityEngine;

public class InfiniteRoadMenu : MonoBehaviour
{
    public GameObject[] roadSegments;  // Массив сегментов дороги
    public float speed = 5f;           // Скорость движения сегментов
    public float segmentLength = 2.95f;  // Длина одного сегмента (можете настроить под свои сегменты)

    public Transform player;            // Ссылка на игрока

    private void FixedUpdate()
    {
        // Двигаем каждый сегмент в зависимости от скорости
        foreach (GameObject segment in roadSegments)
        {
            // Перемещаем сегменты по оси X
            segment.transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);

            // Если сегмент ушел слишком далеко от игрока
            if (segment.transform.position.x < player.position.x - (segmentLength / 2))
            {
                // Перемещаем сегмент в конец массива
                float newX = segment.transform.position.x + (segmentLength * roadSegments.Length);
                segment.transform.position = new Vector3(newX, segment.transform.position.y, segment.transform.position.z);
            }
        }
    }
}

