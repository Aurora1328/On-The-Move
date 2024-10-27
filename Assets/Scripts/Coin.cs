using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;

    // ��������� ���� ��� AudioSource, ������� ����� ���������� � ����������
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            if (uiManager != null)
            {
                uiManager.AddCoin();
            }

            // ������������� ����, ���� AudioSource ���������� � ����� ��������
            if (audioSource != null && !audioSource.mute)
            {
                audioSource.Play();
            }

            // �������� ������ ����� ��������� ��������, ����� ���� ����� ���������������
            Destroy(gameObject, 0.1f);
        }
    }
}
