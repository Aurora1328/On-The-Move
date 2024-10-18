using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text coinCountText;
    private int coinCount = 0;

    public void AddCoin()
    {
        coinCount++;
        Debug.Log("Coin count increased to: " + coinCount);
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        coinCountText.text = coinCount.ToString();
    }
}
