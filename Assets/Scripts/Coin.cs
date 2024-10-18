using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            if (uiManager != null)
            {
                uiManager.AddCoin();
                Debug.Log("Coin collected!"); 
            }

            Destroy(gameObject);
        }
    }
}
