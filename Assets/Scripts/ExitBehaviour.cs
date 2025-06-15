// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using TMPro;
using UnityEngine;

// <summary>
// Manages the behavior of the end screen UI shown when the player finishes the game.
// Displays final stats such as number of diamonds and stars collected, and total score.
// </summary>
public class ExitBehaviour : MonoBehaviour
{
    // Reference to the end screen UI GameObject
    [SerializeField] GameObject endScreenUI;

    // References to the UI text fields for final stats
    [SerializeField] private TextMeshProUGUI finalDiamondText;
    [SerializeField] private TextMeshProUGUI finalStarText;
    [SerializeField] private TextMeshProUGUI finalPointsText;
    // <summary>
    // Called at the start of the game.
    // Ensures the end screen UI is hidden initially.
    // </summary>
    void Start()
    {
        endScreenUI.SetActive(false);
    }

    // <summary>
    // Displays the end screen and pauses the game.
    // </summary>
    public void ShowEndScreen()
    {
        endScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // <summary>
    // Hides the end screen and resumes the game.
    // </summary>
    public void HideEndScreen()
    {
        endScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }
    // <summary>
    // Updates the final UI text with the player's diamond, star, and score totals.
    // </summary>
    // Number of diamonds collected
    // Number of stars collected
    // Total score
    public void SetFinalStats(int diamonds, int stars, int points)
    {
        finalDiamondText.text = "Diamonds Collected: " + diamonds + " / 20";
        finalStarText.text = "Stars Collected: " + stars + " / 3";
        finalPointsText.text = "Total Score: " + points + " / 350";
    }
}
