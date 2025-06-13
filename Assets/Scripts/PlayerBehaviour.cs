using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    Image keyIcon;
    [SerializeField]

    int currentPoints = 0;
    [SerializeField]
    public Transform spawnLocation;
    // Flag to check if the player can interact with objects
    bool canInteract = false;

    diamondBehaviour currentDiamond = null;

    StarBehaviour currentStar = null;
    KeyBehaviour keyCollected = null;
    bool hasKey = false;
    DoorBehaviour currentDoor = null;

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

        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, interactRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Diamond"))
            {
                canInteract = true;
                currentDiamond = hitObject.GetComponent<diamondBehaviour>();
                currentStar = null;
                keyCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Star"))
            {
                canInteract = true;
                currentStar = hitObject.GetComponent<StarBehaviour>();
                currentDiamond = null;
                keyCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Key"))
            {
                canInteract = true;
                keyCollected = hitObject.GetComponent<KeyBehaviour>();
                currentDiamond = null;
                currentStar = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
            }
        }
        else
        {
            currentDiamond = null;
            currentStar = null;
            keyCollected = null;
            currentDoor = null;
        }

        Debug.DrawRay(rayOrigin, transform.forward * interactRange, Color.green);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spikes"))
        {
            SpikesBehaviour spikes = other.GetComponent<SpikesBehaviour>();
            if (spikes != null)
            {
                spikes.Collect(this);
                Respawn();
                transform.position = spawnLocation.position;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spikes"))
        {
            Debug.Log("Player exited spikes trigger area.");
            // Add any logic you want when the player leaves the spikes area here
        }
    }

    public void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = spawnLocation.position;
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
                keyIcon.gameObject.SetActive(true); // Show the UI image
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
