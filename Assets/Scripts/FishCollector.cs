using UnityEngine;
using TMPro;

public class FishCollector : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    private int fishCount = 0;

    public void AddFish(int points)
    {
        fishCount += points;

        // Update the UI safely
        if (fishText != null)
        {
            fishText.text = "x" + fishCount;
        }
        else
        {
            Debug.LogWarning("FishCollector: fishText not assigned in Inspector!");
        }
    }
}



