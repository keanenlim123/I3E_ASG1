using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    int currentPoints = 0;
    // Flag to check if the player can interact with objects
    bool canInteract = false;

    diamondBehaviour currentDiamond = null;

    StarBehaviour currentStar = null;
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
        else if (other.CompareTag("Star"))
        {
            canInteract = true;
            currentStar = other.GetComponent<StarBehaviour>();
        }
        else if (other.CompareTag("Key"))
        {
            canInteract = true;
            keyCollected = other.GetComponent<KeyBehaviour>();
        }
        else if (other.CompareTag("Door"))
        {
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
            else if (currentStar != null)
            {
                Debug.Log("Interacting with diamond");
                currentStar.Collect(this);
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
    public void ModifyPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints += amt;
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // If the player exits the diamond trigger
        if (other.gameObject == currentDiamond.gameObject)
        {
            canInteract = false;
            currentDiamond = null;
        }

        // If the player exits the door trigger
        else if (currentDoor != null && other.gameObject == currentDoor.gameObject)
        {
            canInteract = false;
            currentDoor = null;
        }

        // Optional: If you exit any other interactable, reset interaction
        else if (other.CompareTag("Key"))
        {
            canInteract = false;
            keyCollected = null;
        }
        else if (other.CompareTag("Star"))
        {
            canInteract = false;
            currentStar = null;
        }
    }

}
