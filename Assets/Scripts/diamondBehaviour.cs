using UnityEngine;

public class diamondBehaviour : MonoBehaviour
{
    // Diamond value that will be added to the player's score
    [SerializeField]
    int diamondValue = 1;

    // Method to collect the diamond
    // This method will be called when the player interacts with the diamond
    // It takes a PlayerBehaviour object as a parameter
    // This allows the diamond to modify the player's score
    // The method is public so it can be accessed from other scripts
    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Diamond collected!");

        // Add the diamond value to the player's score
        // This is done by calling the ModifyScore method on the player object
        // The diamond is passed as an argument to the method
        // This allows the player to gain points when they collect the diamond
        player.ModifyScore(diamondValue);

        Destroy(gameObject); // Destroy the diamond object
    }
}
