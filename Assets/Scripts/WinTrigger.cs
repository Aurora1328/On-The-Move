using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public InfiniteRoadMenu InfiniteRoadMenu; // Ссылка на менеджер экрана победы

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект, вошедший в триггер, игроком (например, по тегу "Player")
        if (other.CompareTag("Player"))
        {
            if (InfiniteRoadMenu != null)
            {
                InfiniteRoadMenu.ShowWinScreen(); // Показываем экран победы
            }
        }
    }
}
