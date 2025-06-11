using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    // Player's current health
    int currentHealth = 100;
    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects
    bool canInteract = false;

    diamondBehaviour currentDiamond = null;

    KeyBehaviour keyCollected = null;
    bool hasKey = false;

    DoorBehaviour currentDoor = null;

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the player detects a trigger collider tagged as "Diamond" or "Door"
        if (other.CompareTag("Diamond"))
        {
            // Set the canInteract flag to true
            // Get the DiamondBehaviour component from the detected object
            canInteract = true;
            currentDiamond = other.GetComponent<diamondBehaviour>();
        }
        else if (other.CompareTag("Key"))
        {
            canInteract = true;
            keyCollected = other.GetComponent<KeyBehaviour>();
        }
        else if (other.CompareTag("Door"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
    }
    void OnInteract()
    {
        if (canInteract)
        {
            if (currentDiamond != null)
            {
                Debug.Log("Interacting with diamond");
                currentDiamond.Collect(this);
            }
            else if (keyCollected != null)
            {
                Debug.Log("Interacting with key");
                keyCollected.Collect(this);
                hasKey = true;
            }
            else if (currentDoor != null)
            {
                if (hasKey)
                {
                    Debug.Log("Interacting with door");
                    currentDoor.Interact();
                }
                else
                {
                    Debug.Log("You need a key to open the door!");
                }
            }
        }
    }


    // Method to modify the player's score
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current score
    // The method is public so it can be accessed from other scripts
    public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;
    }

    // Method to modify the player's health
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current health
    // The method is public so it can be accessed from other scripts
    public void ModifyHealth(int amount)
    {
        // Check if the current health is less than the maximum health
        // If it is, increase the current health by the amount passed as an argument
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            // Check if the current health exceeds the maximum health
            // If it does, set the current health to the maximum health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected diamond
        if (currentDiamond != null)
        {
            // If the object that exited the trigger is the same as the current diamond
            if (other.gameObject == currentDiamond.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current diamond to null
                // This prevents the player from interacting with the diamond
                canInteract = false;
                currentDiamond = null;
            }
            else
            {
                canInteract = false;
            }
        }
    }
}
