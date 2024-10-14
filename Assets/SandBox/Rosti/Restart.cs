using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen;

    public Button restartButton;

    public CustomCharacterControllerRosti characterController;

    private void Awake()
    {
        gameOverScreen.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;

        characterController.ResetCharacter();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
