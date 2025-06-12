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

    SpikesBehaviour currentSpikes = null;

    [SerializeField]
    float interactRange = 2f;
    [SerializeField]
    float rayHeightOffset = 1.0f;

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button
    void Update()
    {
        RaycastHit hit;
        canInteract = false;

        // Cast a ray from the player forward
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f; // Adjust the height (e.g. 1.0f)
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, interactRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check for each interactable type
            if (hitObject.CompareTag("Diamond"))
            {
                canInteract = true;
                currentDiamond = hitObject.GetComponent<diamondBehaviour>();
                currentStar = null;
                keyCollected = null;
                currentDoor = null;
                currentSpikes = null;
            }
            else if (hitObject.CompareTag("Star"))
            {
                canInteract = true;
                currentStar = hitObject.GetComponent<StarBehaviour>();
                currentDiamond = null;
                keyCollected = null;
                currentDoor = null;
                currentSpikes = null;
            }
            else if (hitObject.CompareTag("Key"))
            {
                canInteract = true;
                keyCollected = hitObject.GetComponent<KeyBehaviour>();
                currentDiamond = null;
                currentStar = null;
                currentDoor = null;
                currentSpikes = null;
            }
            else if (hitObject.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                currentSpikes = null;
            }
            else if (hitObject.CompareTag("Spikes"))
            {
                canInteract = true;
                currentSpikes = hitObject.GetComponent<SpikesBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                currentDoor = null;
            }
        }
        else
        {
            // Clear references if nothing is hit
            canInteract = false;
            currentDiamond = null;
            currentStar = null;
            keyCollected = null;
            currentDoor = null;
            currentSpikes = null;
        }
        Debug.DrawRay(rayOrigin, transform.forward * interactRange, Color.green);
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
                Debug.Log("Interacting with star");
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

    public void MinusPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints -= amt;
    }

}
