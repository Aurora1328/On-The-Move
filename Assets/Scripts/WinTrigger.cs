using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public InfiniteRoadMenu InfiniteRoadMenu; // ������ �� �������� ������ ������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������, �������� � �������, ������� (��������, �� ���� "Player")
        if (other.CompareTag("Player"))
        {
            if (InfiniteRoadMenu != null)
            {
                InfiniteRoadMenu.ShowWinScreen(); // ���������� ����� ������
            }
        }
    }
}
