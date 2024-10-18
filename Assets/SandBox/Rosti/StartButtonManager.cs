using UnityEngine;

public class StartButtonManager : MonoBehaviour
{
    public CustomCharacterControllerRosti characterController;
    public InfiniteRoadMenu roadMenu;

    public void OnStartButtonPressed()
    {
        roadMenu.OnStartButtonPressed();

        characterController.StartGame();
    }
}