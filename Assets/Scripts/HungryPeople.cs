using UnityEngine;

public class HungryPeople : MonoBehaviour
{
    public float hungerLevel = 100f; // Уровень голода
    public float hungerDecreaseRate = 1f; // Скорость уменьшения голода
    public float feedAmount = 50f; // Сколько голода восполняет еда

    [SerializeField] // Позволяет редактировать переменную в инспекторе
    private float detectionRadius = 5f; // Радиус обнаружения еды

    public LayerMask foodLayer; // Слой еды
    private bool hasEaten = false; // Флаг, чтобы убедиться, что HungryPeople поел

    // Ссылка на скрипт игрока
    private CustomCharacterControllerRosti playerController;

    private void Start()
    {
        // Найдите объект игрока и получите ссылку на его скрипт
        playerController = FindObjectOfType<CustomCharacterControllerRosti>();
    }

    private void Update()
    {
        // Уменьшаем уровень голода со временем
        hungerLevel -= hungerDecreaseRate * Time.deltaTime;

        // Проверяем, не умер ли персонаж от голода
        if (hungerLevel <= 0)
        {
            Die();
        }

        // Ищем еду
        DetectFood();
    }

    private void DetectFood()
    {
        // Проверяем, есть ли еда в радиусе обнаружения
        Collider[] foodColliders = Physics.OverlapSphere(transform.position, detectionRadius, foodLayer);
        foreach (var foodCollider in foodColliders)
        {
            // Если нашли еду и HungryPeople ещё не поел
            if (!hasEaten)
            {
                EatFood(foodCollider.gameObject);
                break; // Если нашли еду, выходим из цикла
            }
        }
    }

    private void EatFood(GameObject food)
    {
        // Увеличиваем уровень голода и уничтожаем еду
        hungerLevel += feedAmount;
        hasEaten = true; // Устанавливаем флаг, что еда была съедена
        Destroy(food); // Удаляем еду из сцены
        Debug.Log($"{gameObject.name} has eaten food and is now satisfied!");
        // Можно добавить дополнительную логику для исчезновения или завершения игры
        Destroy(gameObject); // Удаляем HungryPeople из сцены
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died from hunger!");
        // Здесь вы можете добавить логику для смерти персонажа
        Destroy(gameObject); // Удаляем персонажа из сцены
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Убедитесь, что у вашего игрока установлен тег "Player"
        {
            // Если HungryPeople не сыт, уничтожаем игрока
            if (hungerLevel > 0 && !hasEaten)
            {
                Debug.Log($"{gameObject.name} has eaten the player!");
                // Вызываем метод EndGame() у скрипта игрока
                playerController.EndGame();
                // Уничтожаем HungryPeople
            }
        }
    }

    // Метод для изменения радиуса обнаружения
    public void SetDetectionRadius(float newRadius)
    {
        detectionRadius = newRadius;
    }
}
