using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactPromptText;

    [SerializeField]
    Image keyIcon;

    [SerializeField]
    Image bootsIcon;

    [SerializeField] private TextMeshProUGUI diamondCounterText;
    [SerializeField] private TextMeshProUGUI starCounterText;

    private int diamondCount = 0;
    private int starCount = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]

    int currentPoints = 0;
    [SerializeField]
    public Transform spawnLocation;
    // Flag to check if the player can interact with objects
    bool canInteract = false;


    diamondBehaviour currentDiamond = null;

    StarBehaviour currentStar = null;
    KeyBehaviour keyCollected = null;

    BootsBehaviour bootsCollected = null;
    bool hasKey = false;

    bool hasBoots = false;
    DoorBehaviour currentDoor = null;

    [SerializeField]
    float interactRange = 2f;
    [SerializeField]
    float rayHeightOffset = 1.0f;
    DoorBehaviour1 currentDoor2 = null;




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
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Star"))
            {
                canInteract = true;
                currentStar = hitObject.GetComponent<StarBehaviour>();
                currentDiamond = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Key"))
            {
                canInteract = true;
                interactPromptText.text = "Press E to pick up Key!";
                interactPromptText.gameObject.SetActive(true);
                keyCollected = hitObject.GetComponent<KeyBehaviour>();
                currentDiamond = null;
                currentStar = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Door2"))
            {
                canInteract = true;
                currentDoor2 = hitObject.GetComponent<DoorBehaviour1>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Boots"))
            {
                canInteract = true;
                interactPromptText.text = "Press E to pick up boots!";
                interactPromptText.gameObject.SetActive(true);
                bootsCollected = hitObject.GetComponent<BootsBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
            }
        }
        else
        {
            currentDiamond = null;
            currentStar = null;
            keyCollected = null;
            bootsCollected = null;
            currentDoor = null;
            currentDoor2 = null;
            interactPromptText.gameObject.SetActive(false);

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
                Debug.Log("Teleport");
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
                diamondCount++;
                UpdateDiamondUI();
            }
            else if (currentStar != null)
            {
                Debug.Log("Interacting with star");
                currentStar.Collect(this);
                starCount++;
                UpdateStarUI();
            }
            else if (bootsCollected != null)
            {
                Debug.Log("Interacting with boots");
                bootsCollected.Collect(this);
                hasBoots = true;
                bootsIcon.gameObject.SetActive(true);
            }

            else if (keyCollected != null)
            {
                Debug.Log("Interacting with key");
                keyCollected.Collect(this);
                hasKey = true;
                keyIcon.gameObject.SetActive(true); // Show the UI image
            }
            else if (currentDoor2 != null)
            {
                Debug.Log("Interacting with Door2");
                currentDoor2.OpenDoor();
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

    // Function to check player haveboots
    public bool HasBoots()
    {
        return hasBoots;
    }

    // Method to modify the player's score
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current score
    // The method is public so it can be accessed from other scripts
    public void ModifyPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints += amt;
        UpdateScoreUI();
    }

    public void MinusPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints -= amt;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Points: " + currentPoints.ToString() + " / 350";
    }
    void UpdateDiamondUI()
    {
        diamondCounterText.text = "Diamonds: " + diamondCount + " / 20";
    }

    void UpdateStarUI()
    {
        starCounterText.text = "Stars: " + starCount + " /3";
    }
}
