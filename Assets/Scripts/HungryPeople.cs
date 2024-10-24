using UnityEngine;

public class HungryPeople : MonoBehaviour
{
    public float hungerLevel = 100f;
    public float feedAmount = 50f;

    [SerializeField]
    private float detectionRadius = 5f;

    public LayerMask foodLayer;
    private bool hasEaten = false;

    private CustomCharacterControllerRosti playerController;

    private void Start()
    {
        playerController = FindObjectOfType<CustomCharacterControllerRosti>();
    }

    private void Update()
    {
        // Удаляем уменьшение голода
        // hungerLevel -= hungerDecreaseRate * Time.deltaTime;

        if (hungerLevel <= 0)
        {
            Die();
        }

        DetectFood();
    }

    private void DetectFood()
    {
        Collider[] foodColliders = Physics.OverlapSphere(transform.position, detectionRadius, foodLayer);
        foreach (var foodCollider in foodColliders)
        {
            if (!hasEaten)
            {
                EatFood(foodCollider.gameObject);
                break;
            }
        }
    }

    private void EatFood(GameObject food)
    {
        hungerLevel += feedAmount;
        hasEaten = true;
        Destroy(food);
        Debug.Log($"{gameObject.name} has eaten food and is now satisfied!");
        Destroy(gameObject);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died from hunger!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hungerLevel > 0 && !hasEaten)
            {
                Debug.Log($"{gameObject.name} has eaten the player!");
                playerController.EndGame();
            }
        }
    }

    public void SetDetectionRadius(float newRadius)
    {
        detectionRadius = newRadius;
    }
}
