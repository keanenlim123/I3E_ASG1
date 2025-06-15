using TMPro;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour
{
    [SerializeField] GameObject endScreenUI;

    [SerializeField] private TextMeshProUGUI finalDiamondText;
    [SerializeField] private TextMeshProUGUI finalStarText;
    [SerializeField] private TextMeshProUGUI finalPointsText;

    void Start()
    {
        endScreenUI.SetActive(false);
    }

    public void ShowEndScreen()
    {
        endScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideEndScreen()
    {
        endScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetFinalStats(int diamonds, int stars, int points)
    {
        finalDiamondText.text = "Diamonds Collected: " + diamonds + " / 20";
        finalStarText.text = "Stars Collected: " + stars + " / 3";
        finalPointsText.text = "Total Score: " + points + " / 350";
    }
}
